using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Weixin.Api.FormTest
{
    public partial class Form1 : Form
    {
        IWeixinClient client = new DefaultWeixinClient();
        string uuid = "";
        Entity.LoginWxResponse _loginWxResponse = null;
        Entity.WxSyncKey _syncKey = null;
        Entity.WxSyncKey _syncCheckKey = null;

        string _passTicket = "";
        string _myUserName = "";
        string _toUser = "";

        Dictionary<string, string> _userMsgMapping = new Dictionary<string, string>();

        //Entity.GetContactResponse _contacts = null;

        System.Collections.Queue _queue = new System.Collections.Queue();

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            WriteLog("=============================================================");

            this.button1.Enabled = false;
            this.textBox1.Text = "";
            client.SetCookie(null);

            var _isNeedRefreshQRCode = true;
            var _start = DateTime.Now;

            while (true)
            {
                if (_isNeedRefreshQRCode)
                {
                    SetText("获取UUID...");
                    var request = new Entity.GetQRCodeRequest();
                    var response = await client.ExecuteAsync(request);
                    SetText(response.QRLoginUUId);
                    uuid = response.QRLoginUUId;

                    SetText("获取二维码...");
                    var r = new Entity.GetQRImageRequest(response.QRLoginUUId);
                    var re = await client.ExecuteAsync(r);
                    this.pictureBox1.Image = re.QRImage;

                    _isNeedRefreshQRCode = false;
                }

                var _scanStart = DateTime.Now;
                SetText("等待扫描二维码...");
                var _request = new Entity.ScanQRRequest(1, uuid);
                var _response = await client.ExecuteAsync(_request);
                if (_response.Code == "408")
                {
                    SetText("扫描超时...{0}s", DateTime.Now.Subtract(_scanStart).TotalSeconds);
                    continue;
                }
                else if (_response.Code == "201")
                {
                    SetText("扫描成功,等待确认登录...");
                    var bytes = Convert.FromBase64String(_response.UserAvatarBase64String);
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
                    {
                        this.pictureBox1.Image = Image.FromStream(ms);
                    }
                    break;
                }
                else if (_response.Code == "400")
                {
                    SetText("二维码已经过期...{0}s没有扫描", DateTime.Now.Subtract(_start).TotalSeconds);
                    _isNeedRefreshQRCode = true;
                    continue;
                }
            }

            Entity.ScanQRResponse _scanResponse = null;

            while (true)
            {
                SetText("等待确认登录...");
                var _request = new Entity.ScanQRRequest(0, uuid);
                _scanResponse = await client.ExecuteAsync(_request);
                if (_scanResponse.Code == "408")
                {
                    SetText("登录超时...");
                    continue;
                }
                else if (_scanResponse.Code == "200")
                {
                    SetText("已经确认登录...");
                    break;
                }
            }

            SetText("开始登录...");
            var _loginRequest = new Entity.LoginWxRequest(_scanResponse.RedirectUri);
            var _loginResponse = await client.ExecuteAsync(_loginRequest);

            _loginWxResponse = _loginResponse;
            _passTicket = _loginWxResponse.PassTicket;

            SetText(_loginResponse.SKey);

            SetText("初始化...");
            var _initRequest = new Entity.WxInitRequest(new Entity.WxRequest(_loginResponse));
            var _initResponse = await client.ExecuteAsync(_initRequest);
            if (_initResponse.BaseResponse.Ret != 0)
            {
                SetText("初始化失败！");
                return;
            }
            SetText("联系人数量：{0}", _initResponse.Count);
            foreach (var c in _initResponse.ContactList)
            {
                SetText("初始化联系人信息\tUserName:{0}\tAlias:{1}\tDisplayName:{2}\tNickName:{3}\tRemarkName:{4}", c.UserName, c.Alias, c.DisplayName, c.NickName, c.RemarkName);
            }

            _syncKey = _initResponse.SyncKey;
            _syncCheckKey = _initResponse.SyncKey;
            _myUserName = _initResponse.User.UserName;

            SetText("开通微信状态通知...");
            var _openNotifyRequest = new Entity.OpenWxStatusNotifyRequest(new Entity.WxRequest(_loginResponse), _initResponse.User.UserName);
            var _openNotifyResponse = await client.ExecuteAsync(_openNotifyRequest);
            SetText(_openNotifyResponse.MsgID);

            SetText("获取联系人...");
            var _getContactRequest = new Entity.GetContactRequest(_loginResponse.SKey, 0);
            var _getContactResponse = await client.ExecuteAsync(_getContactRequest);
            SetText("联系人数量：{0}", _getContactResponse.MemberCount);
            foreach (var c in _getContactResponse.MemberList)
            {
                SetText("联系人信息\tUserName:{0}\tAlias:{1}\tDisplayName:{2}\tNickName:{3}\tRemarkName:{4}", c.UserName, c.Alias, c.DisplayName, c.NickName, c.RemarkName);
            }

            var f = _getContactResponse.MemberList.FirstOrDefault(p => p.NickName == "王小重");
            if (f != null)
                _toUser = f.UserName;

            SetText("获取群组信息...");
            var gs = _initResponse.GetChatSetArray().Where(p => p.StartsWith("@@"));
            if (gs != null && gs.Count() > 0)
            {
                var gss = from n in gs select new Entity.BatchGetContactRequest._BatchGetContactRequestItem { EncryChatRoomId = "", UserName = n };
                var _batchGetContactRequest = new Entity.BatchGetContactRequest(new Entity.WxRequest(_loginResponse), gss.ToArray(), _loginResponse.PassTicket);
                var _batchGetContactResponse = await client.ExecuteAsync(_batchGetContactRequest);
                SetText("{0}", _batchGetContactResponse.Count);
                foreach (var c in _batchGetContactResponse.ContactList)
                {
                    SetText("群组信息\tUserName:{0}\tAlias:{1}\tDisplayName:{2}\tNickName:{3}\tRemarkName:{4}", c.UserName, c.Alias, c.DisplayName, c.NickName, c.RemarkName);
                }
            }

            SetText("获取其它群组信息...");
            gs = _getContactResponse.MemberList.Where(p => p.UserName.StartsWith("@@")).Select(p => p.UserName).ToArray();
            if (gs != null && gs.Count() > 0)
            {
                var gss = from n in gs select new Entity.BatchGetContactRequest._BatchGetContactRequestItem { EncryChatRoomId = "", UserName = n };
                var _batchGetContactRequest = new Entity.BatchGetContactRequest(new Entity.WxRequest(_loginResponse), gss.ToArray(), _loginResponse.PassTicket);
                var _batchGetContactResponse = await client.ExecuteAsync(_batchGetContactRequest);
                SetText("{0}", _batchGetContactResponse.Count);
                foreach (var c in _batchGetContactResponse.ContactList)
                {
                    SetText("群组信息\tUserName:{0}\tAlias:{1}\tDisplayName:{2}\tNickName:{3}\tRemarkName:{4}", c.UserName, c.Alias, c.DisplayName, c.NickName, c.RemarkName);
                }
            }

            this.button2.Enabled = true;
            this.button3.Enabled = true;
        }

        private void SetText(string text)
        {
            var log = DateTime.Now.ToString("HH:mm:ss.fff") + " " + text + "\r\n";
            this.textBox1.AppendText(log);
            WriteLog(log);
        }

        private void SetText(string text, params object[] paras)
        {
            var log = DateTime.Now.ToString("HH:mm:ss.fff") + " " + string.Format(text, paras) + "\r\n";
            this.textBox1.AppendText(log);
            WriteLog(log);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            this.button2.Enabled = false;

            DoTask();

            while (true)
            {
                var syncRequest = new Entity.SyncCheckRequest(new Entity.WxRequest(_loginWxResponse), _syncCheckKey.List);
                var syncResponse = await client.ExecuteAsync(syncRequest);

                if (syncResponse.RetCode == 0 && syncResponse.Selector > 0)
                {
                    var syncMsgRequest = new Entity.WxSyncRequest(new Entity.WxRequest(_loginWxResponse), _passTicket, _syncKey, _loginWxResponse.WxSid, _loginWxResponse.SKey);
                    var syncMsgResponse = await client.ExecuteAsync(syncMsgRequest);

                    WriteLog(System.Web.HttpUtility.HtmlDecode(Helper.SerializationHelper.SerializeObjectToJson(syncMsgResponse)));

                    if (syncMsgResponse.BaseResponse.Ret == 0)
                    {
                        _syncKey = syncMsgResponse.SyncKey;
                        _syncCheckKey = syncMsgResponse.SyncCheckKey;

                        foreach (var msg in syncMsgResponse.AddMsgList)
                        {
                            if (msg.MsgType == 1)
                            {
                                #region text msg
                                SetText("收到来自{0}的消息：{1}", msg.FromUserName, msg.Content);

                                if (_userMsgMapping.Keys.Contains(msg.FromUserName))
                                    _userMsgMapping[msg.FromUserName] = msg.Content;
                                else
                                    _userMsgMapping.Add(msg.FromUserName, msg.Content);
                                #endregion
                            }
                            else if (msg.MsgType == 3)
                            {
                                #region img msg
                                SetText("收到来自{0}的图片：{1}", msg.FromUserName, Helper.SerializationHelper.SerializeObjectToJson(Helper.SerializationHelper.DeserializeXML<Entity.WxImgMsgContent>(msg.Content, true, "<br/>")));

                                SetText("给对方发送确认信息...");
                                var _sendTextRequest = new Entity.WxSendMsgRequest
                                {
                                    BaseRequest = new Entity.WxBaseRequest(new Entity.WxRequest(_loginWxResponse)),
                                    PassTicket = _passTicket,
                                    Message = new Entity.WxMsg
                                    {
                                        FromUserName = _myUserName,
                                        ToUserName = msg.FromUserName,
                                        Content = "您的图片已经收到，本地保存后会回复给您。"
                                    }
                                };

                                var _sendTextResponse = await client.ExecuteAsync(_sendTextRequest);
                                if (_sendTextResponse.BaseResponse.Ret == 0)
                                    SetText("成功发送确认信息!");

                                SetText("下载图片...");
                                var _downloadPicRequest = new Entity.GetMsgImgRequest(msg.MsgId, _loginWxResponse.SKey, "_");
                                var _downloadPicResponse = await client.ExecuteAsync(_downloadPicRequest);

                                var _img = _downloadPicResponse.HeadImage;
                                var _ext = ".png";
                                if (_img.RawFormat == System.Drawing.Imaging.ImageFormat.Bmp)
                                    _ext = ".bmp";
                                else if (_img.RawFormat == System.Drawing.Imaging.ImageFormat.Png)
                                    _ext = ".png";
                                else if (_img.RawFormat == System.Drawing.Imaging.ImageFormat.Jpeg)
                                    _ext = ".jpg";
                                else if (_img.RawFormat == System.Drawing.Imaging.ImageFormat.Gif)
                                    _ext = ".gif";

                                SetText("保存图片...");
                                var _file = System.IO.Path.Combine(Application.StartupPath, "images", Guid.NewGuid().ToString() + _ext);
                                //_downloadPicResponse.HeadImage.Save(_file);
                                var _finfo = new System.IO.FileInfo(_file);
                                if (!_finfo.Directory.Exists)
                                    _finfo.Directory.Create();

                                System.IO.File.WriteAllBytes(_file, Convert.FromBase64String(_downloadPicResponse.ResponseBase64String));

                                SetText("返回图片给对方...");
                                SetText("上传图片...");
                                var _uploadImgRequest = new Entity.UploadMediaRequest(
                                    new System.IO.FileInfo(_file),
                                    new Entity.WxRequest(_loginWxResponse),
                                    _myUserName, msg.FromUserName, client.GetWxDataTicket(), _passTicket);
                                var _uploadImgResponse = await client.ExecuteAsync(_uploadImgRequest);
                                if (_uploadImgResponse.BaseResponse.Ret == 0)
                                {
                                    SetText("上传成功!");
                                }

                                SetText("发送图片...");
                                var _sendImgRequest = new Entity.WxSendImgMsgRequest
                                {
                                    BaseRequest = new Entity.WxBaseRequest(new Entity.WxRequest(_loginWxResponse)),
                                    PassTicket = _passTicket,
                                    Message = new Entity.WxImgMsg
                                    {
                                        FromUserName = _myUserName,
                                        ToUserName = msg.FromUserName,
                                        MediaId = _uploadImgResponse.MediaId
                                    }
                                };
                                var _sendImgResponse = await client.ExecuteAsync(_sendImgRequest);
                                SetText("成功...{0}", _sendImgResponse.MsgID);
                                #endregion
                            }
                            else if (msg.MsgType == 37)
                            {
                                #region add friend
                                SetText("收到好友添加信息...");
                                if (syncMsgResponse.AddMsgList[0].RecommendInfo.Content == "12345678")
                                {
                                    var _verifyUserRequest = new Entity.VerifyUserRequest(new Entity.WxRequest(_loginWxResponse), syncMsgResponse.AddMsgList[0].RecommendInfo, _passTicket);
                                    var _verifyUserResponse = await client.ExecuteAsync(_verifyUserRequest);
                                    if (_verifyUserResponse.BaseResponse.Ret == 0)
                                    {
                                        SetText("已经自动通过!");
                                        SetText("发送打招呼内容");
                                        var _sayHello = await client.ExecuteAsync(new Entity.WxSendMsgRequest(new Entity.WxMsg() { Content = "Hi, I'm Jarvis", FromUserName = _myUserName, ToUserName = syncMsgResponse.AddMsgList[0].RecommendInfo.UserName }, _passTicket));
                                        if (_sayHello.BaseResponse.Ret == 0)
                                            SetText("打招呼成功");
                                    }
                                }
                                #endregion
                            }
                            else if(msg.MsgType == 49)
                            {
                                #region app msg

                                try
                                {
                                    var _appMsg = Helper.SerializationHelper.DeserializeXML<Entity.WxAppMsgContent>(msg.Content, true, "<br/>");
                                    if (_appMsg != null && _appMsg.AppInformation.AppName == "优酷视频")
                                    {
                                        SetText("收到一条分享的优酷视频:\r\n\t标题:{0}\r\n\tUrl:{1}",
                                            _appMsg.AppMsg.Title, _appMsg.AppMsg.Url);

                                        SetText("给对方发送确认信息...");
                                        var _sendTextRequest = new Entity.WxSendMsgRequest
                                        {
                                            BaseRequest = new Entity.WxBaseRequest(new Entity.WxRequest(_loginWxResponse)),
                                            PassTicket = _passTicket,
                                            Message = new Entity.WxMsg
                                            {
                                                FromUserName = _myUserName,
                                                ToUserName = msg.FromUserName,
                                                Content = "收到您分享的视频。\n\n标题：\n" + _appMsg.AppMsg.Title + "\n\nUrl：\n" + _appMsg.AppMsg.Url + "\n\n后台会自动转存到共享账号,转存成功后会微信通知您。"
                                            }
                                        };
                                        await client.ExecuteAsync(_sendTextRequest);

                                        var s = new VideoTask
                                        {
                                            LoginResponse = _loginWxResponse,
                                            MyUserName = _myUserName,
                                            ToUserName = msg.FromUserName,
                                            Client = client,
                                            PassTicket = _passTicket,
                                            VideoTitle = _appMsg.AppMsg.Title,
                                            VideoUrl = _appMsg.AppMsg.Url
                                        };

                                        _queue.Enqueue(s);
                                    }
                                }
                                catch
                                {

                                }

                                #endregion
                            }
                        }
                    }
                }
                else if(syncResponse.RetCode != 0)
                {
                    SetText("已经退出.");
                    break;
                }
            }

            client.SetCookie(null);
            this.button1.Enabled = true;
            this.button2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.button3.Enabled = false;

            timer1.Interval = 60000;
            timer1.Start();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            if (now.Hour >= 18 && now.Hour <= 24 && now.DayOfWeek != DayOfWeek.Saturday && now.DayOfWeek != DayOfWeek.Sunday)
            {
                if (!_userMsgMapping.Keys.Contains(_toUser) || _userMsgMapping[_toUser] != "朕知道了")
                {
                    var _sendTextRequest = new Entity.WxSendMsgRequest
                    {
                        BaseRequest = new Entity.WxBaseRequest(new Entity.WxRequest(_loginWxResponse)),
                        PassTicket = _passTicket,
                        Message = new Entity.WxMsg
                        {
                            FromUserName = _myUserName,
                            ToUserName = "filehelper",
                            Content = "记着打卡！记着打卡！记着打卡！"
                        }
                    };

                    await client.ExecuteAsync(_sendTextRequest);
                }
            }
        }

        private static readonly object _lockObj = new object();
        private void WriteLog(string text)
        {
            var logFile = System.IO.Path.Combine(Application.StartupPath, "logs", DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
            var file = new System.IO.FileInfo(logFile);
            if (!file.Directory.Exists)
                file.Directory.Create();

            lock (_lockObj)
            {
                System.IO.File.AppendAllText(logFile, text);
                if (!text.EndsWith("\r\n"))
                    System.IO.File.AppendAllText(logFile, "\r\n");
            }
        }

        private void DoTask()
        {
            new System.Threading.Tasks.TaskFactory().StartNew(new Action(() =>
            {
                SetText("视频监控已启动...");
                while (true)
                {
                    if (_queue.Count > 0)
                    {
                        var task = _queue.Dequeue() as VideoTask;
                        if (task == null)
                            continue;

                        SetText("启动任务-{0}", task.VideoUrl);

                        task.UploadVideo();
                    }

                    System.Threading.Tasks.Task.Delay(5000).Wait();
                    SetText("视频监控在运行...");
                }
            }));
        }
    }
}
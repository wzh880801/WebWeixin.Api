using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weixin.Api;
using System.Diagnostics;
using System.IO;

namespace Weixin.Api.FormTest
{
    public class VideoTask
    {
        public Entity.LoginWxResponse LoginResponse { get; set; }

        public string PassTicket { get; set; }

        public IWeixinClient Client { get; set; }

        public string MyUserName { get; set; }

        public string ToUserName { get; set; }

        public string VideoUrl { get; set; }

        public string VideoTitle { get; set; }

        public void UploadVideo()
        {
            var _path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var _uploadApp = Path.Combine(_path, "Video.Test.exe");

            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = _uploadApp;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WorkingDirectory = _path;
            p.StartInfo.Arguments = string.Format(" {0}", this.VideoUrl);
            p.Start();
            p.WaitForExit();

            var _sendTextRequest = new Entity.WxSendMsgRequest
            {
                BaseRequest = new Entity.WxBaseRequest(new Entity.WxRequest(this.LoginResponse)),
                PassTicket = this.PassTicket,
                Message = new Entity.WxMsg
                {
                    FromUserName = this.MyUserName,
                    ToUserName = this.ToUserName,
                    Content = "您分享的视频。\n\n标题：\n" + this.VideoTitle + "\n\nUrl：\n" + this.VideoUrl + "\n\n已经成功上传到共享账号。"
                }
            };
            this.Client.Execute(_sendTextRequest);
        }
    }
}

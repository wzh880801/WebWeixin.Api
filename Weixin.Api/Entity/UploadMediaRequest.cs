using System;
using System.IO;
using System.Text;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    public class UploadMediaRequest : WeixinRequest<UploadMediaResponse>
    {
        public override string Accept
        {
            get
            {
                return "*/*";
            }
        }

        public override string QueryString
        {
            get
            {
                return "";
            }
        }

        public override string ApiUrl
        {
            get
            {
                return string.Format("https://file.{0}/cgi-bin/mmwebwx-bin/webwxuploadmedia?f=json", this.Host);
            }
        }

        /// <summary>
        /// IWeixinClient会自动设置Host，手工设置的值会被覆盖
        /// </summary>
        public string Host { get; set; }

        private static int _fileIndex = 0;

        public string FromUserName { get; set; }

        public string ToUserName { get; set; }

        public override byte[] QueryBytes
        {
            get
            {
                var fileBytes = File.ReadAllBytes(_fileInfo.FullName);
                var request = new _UploadMediaRequest
                {
                    BaseRequest = this.BaseRequest,
                    DataLen = fileBytes.Length,
                    TotalLen = fileBytes.LongLength,
                    FileMd5 = Helper.FileHelper.GetFileMd5(_fileInfo),
                    FromUserName = this.FromUserName,
                    ToUserName = this.ToUserName,
                };
                var mediaType = Helper.FileHelper.GetMediaType(_fileInfo);
                var fileType = Helper.FileHelper.GetFileType(_fileInfo);

                var body = new StringBuilder();

                var _rowBoundary= "--" + _boundary;
                body.AppendLine(_rowBoundary);
                body.AppendLine("Content-Disposition: form-data; name=\"id\"");
                body.AppendLine();
                body.AppendLine(string.Format("WU_FILE_{0}", _fileIndex));
                _fileIndex++;

                body.AppendLine(_rowBoundary);
                body.AppendLine("Content-Disposition: form-data; name=\"name\"");
                body.AppendLine();
                body.AppendLine(this._fileInfo.Name);

                body.AppendLine(_rowBoundary);
                body.AppendLine("Content-Disposition: form-data; name=\"type\"");
                body.AppendLine();
                body.AppendLine(fileType);

                body.AppendLine(_rowBoundary);
                body.AppendLine("Content-Disposition: form-data; name=\"lastModifiedDate\"");
                body.AppendLine();
                body.AppendLine(Common.ExDateTimeMethod.ToUpLoadString(_fileInfo.LastWriteTime));

                body.AppendLine(_rowBoundary);
                body.AppendLine("Content-Disposition: form-data; name=\"size\"");
                body.AppendLine();
                body.AppendLine(fileBytes.Length.ToString());

                body.AppendLine(_rowBoundary);
                body.AppendLine("Content-Disposition: form-data; name=\"mediatype\"");
                body.AppendLine();
                body.AppendLine(mediaType);

                body.AppendLine(_rowBoundary);
                body.AppendLine("Content-Disposition: form-data; name=\"uploadmediarequest\"");
                body.AppendLine();
                body.AppendLine(Newtonsoft.Json.JsonConvert.SerializeObject(request));

                body.AppendLine(_rowBoundary);
                body.AppendLine("Content-Disposition: form-data; name=\"webwx_data_ticket\"");
                body.AppendLine("");
                body.AppendLine(this.WebWxDataTicket);

                body.AppendLine(_rowBoundary);
                body.AppendLine("Content-Disposition: form-data; name=\"pass_ticket\"");
                body.AppendLine();
                body.AppendLine(this.PassTicket);

                body.AppendLine(_rowBoundary);
                body.AppendLine("Content-Disposition: form-data; name=\"filename\"; filename=\"" + _fileInfo.Name + "\"");
                body.AppendLine("Content-Type: " + fileType);
                body.AppendLine();

                //file bytes or contents
                var ext = _fileInfo.Extension.ToLower();
                if (ext == ".txt" || fileType == ".csv")
                {
                    body.AppendLine(System.Text.Encoding.UTF8.GetString(fileBytes));
                    body.AppendLine();
                    body.AppendLine("\r\n" + _rowBoundary + "--");
                    body.AppendLine();

                    return System.Text.Encoding.UTF8.GetBytes(body.ToString());
                }
                else
                {
                    byte[] bytes1 = System.Text.Encoding.UTF8.GetBytes(body.ToString());
                    byte[] bytes2 = fileBytes;
                    byte[] bytes3 = System.Text.Encoding.UTF8.GetBytes("\r\n" + _rowBoundary + "--\r\n");

                    var _bytes = new System.Collections.Generic.List<byte>();
                    _bytes.AddRange(bytes1);
                    _bytes.AddRange(bytes2);
                    _bytes.AddRange(bytes3);

                    return _bytes.ToArray();
                }
            }
        }

        public string WebWxDataTicket { get; set; }
        public string PassTicket { get; set; }

        //multipart/form-data; boundary=----WebKitFormBoundary6UmvJDeU0pdEBlli
        private string _boundary = "----WebKitFormBoundary6UmvJDeU0pdEBlli";

        private FileInfo _fileInfo = null;

        public override string ContentTypeHeader
        {
            get
            {
                return "multipart/form-data; boundary=" + _boundary;
            }
        }

        public UploadMediaRequest()
            : base()
        {
            this.Host = "wx2.qq.com";
            this.Referer = "https://wx2.qq.com/?&lang=zh_CN";
            this.Origin = "https://wx2.qq.com/";
            this.KeepAlive = true;
            this.HttpMethod = HttpMethods.POST;

            _boundary = string.Format("----WebKitFormBoundary{0}", Guid.NewGuid().ToString("N").Substring(0, 16));
        }

        public UploadMediaRequest(FileInfo file, WxRequest request, string fromUserName, string toUserName, string wxDataTicket, string passTicket)
            : this()
        {
            this._fileInfo = file;
            this.BaseRequest = request;
            this.FromUserName = fromUserName;
            this.ToUserName = toUserName;
            this.WebWxDataTicket = wxDataTicket;
            this.PassTicket = passTicket;
        }

        public WxRequest BaseRequest { get; set; }

        public class _UploadMediaRequest
        {
            public int UploadType { get; set; }
            public WxRequest BaseRequest { get; set; }
            public long ClientMediaId { get; set; }
            public long TotalLen { get; set; }
            public long StartPos { get; set; }
            public long DataLen { get; set; }
            public int MediaType { get; set; }
            public string FromUserName { get; set; }
            public string ToUserName { get; set; }
            public string FileMd5 { get; set; }

            public _UploadMediaRequest()
            {
                this.UploadType = 2;
                this.MediaType = 4;
                this.StartPos = 0;
                this.ClientMediaId = Common.ExDateTime.Timestamp;
            }

            public _UploadMediaRequest(WxRequest request)
                : this()
            {
                this.BaseRequest = request;
            }
        }
    }
}
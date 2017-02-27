using System;

namespace Weixin.Api.Entity
{
    public class LoginWxRequest : WeixinRequest<LoginWxResponse>
    {
        public override string ApiUrl
        {
            get
            {
                return this.LoginUrl;
            }
        }

        public override string QueryString
        {
            get
            {
                return "";
            }
        }

        public override string Accept
        {
            get
            {
                return "text/html, application/xhtml+xml, image/jxr, */*";
            }
        }

        public LoginWxRequest()
            : base()
        {
            this.Referer = "https://wx.qq.com/";
            this.KeepAlive = true;
            this.ContentType = Enum.ContentTypes.NONE;
        }

        public LoginWxRequest(string url) 
            : this()
        {
            this.LoginUrl = url;
        }

        public string LoginUrl { get; set; }
    }
}
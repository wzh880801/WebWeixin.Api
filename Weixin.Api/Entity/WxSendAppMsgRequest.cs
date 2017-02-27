using System;

namespace Weixin.Api.Entity
{
    public class WxSendAppMsgRequest : WeixinRequest<WxSendAppMsgResponse>
    {
        public int Scene { get; set; }

        public WxAppMsg Message { get; set; }

        public WxRequest BaseRequest { get; set; }

        public override string QueryString
        {
            get
            {
                var msg = new _WxSendAppMsgRequest
                {
                    BaseRequest = this.BaseRequest,
                    Scene = this.Scene,
                    Msg = this.Message,
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(msg);
            }
        }

        public override string ApiUrl
        {
            get
            {
                return string.Format("https://{0}/cgi-bin/mmwebwx-bin/webwxsendappmsg?fun=async&f=json&pass_ticket={1}",
                    this.Host,
                    this.PassTicket);
            }
        }

        /// <summary>
        /// IWeixinClient会自动设置Host，手工设置的值会被覆盖
        /// </summary>
        public string Host { get; set; }

        public string PassTicket { get; set; }

        public override string Accept
        {
            get
            {
                return "application/json, text/plain, */*";
            }
        }

        public class _WxSendAppMsgRequest
        {
            public WxRequest BaseRequest { get; set; }

            public int Scene { get; set; }

            public WxAppMsg Msg { get; set; }
        }

        public WxSendAppMsgRequest()
            : base()
        {
            this.Host = "wx2.qq.com";
            this.Referer = "https://wx2.qq.com/?&lang=zh_CN";
            this.Origin = "https://wx2.qq.com";
            this.HttpMethod = Enum.HttpMethods.POST;
            this.ContentType = Enum.ContentTypes.JSON;
            this.KeepAlive = true;
            this.Scene = 0;
        }

        public WxSendAppMsgRequest(WxAppMsg msg, string passTicket, int scene = 0)
            : this()
        {
            this.Message = msg;
            this.PassTicket = passTicket;
            this.Scene = scene;
        }
    }
}
using System;

namespace Weixin.Api.Entity
{
    public class WxStatusNotifyRequest : WeixinRequest<OpenWxStatusNotifyResponse>
    {
        public override string ApiUrl
        {
            get
            {
                return string.Format("https://{0}/cgi-bin/mmwebwx-bin/webwxstatusnotify", this.Host);
            }
        }

        public override string QueryString
        {
            get
            {
                var request = new _WxStatusNotifyRequest
                {
                    BaseRequest = new WxBaseRequest(this.WxRequest),
                    ClientMsgId = Common.ExDateTime.Timestamp,
                    Code = 1,
                    FromUserName = this.FromUserName,
                    ToUserName = this.ToUserName
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(request);
            }
        }

        public override string Accept
        {
            get
            {
                return "application/json, text/plain, */*";
            }
        }

        /// <summary>
        /// IWeixinClient会自动设置Host，手工设置的值会被覆盖
        /// </summary>
        public string Host { get; set; }

        public WxStatusNotifyRequest()
            : base()
        {
            this.Referer = "https://wx2.qq.com/";
            this.Origin = "https://wx2.qq.com/";
            this.KeepAlive = true;
            this.HttpMethod = Enum.HttpMethods.POST;
            this.Host = "wx2.qq.com";
        }

        public WxRequest WxRequest { get; set; }

        public string FromUserName { get; set; }

        public string ToUserName { get; set; }

        public WxStatusNotifyRequest(WxRequest request, string fromUserName, string toUserName)
            : this()
        {
            this.WxRequest = request;
            this.FromUserName = fromUserName;
            this.ToUserName = toUserName;
        }

        public class _WxStatusNotifyRequest
        {
            public WxBaseRequest BaseRequest { get; set; }
            public long ClientMsgId { get; set; }
            public int Code { get; set; }
            public string FromUserName { get; set; }
            public string ToUserName { get; set; }
        }
    }
}
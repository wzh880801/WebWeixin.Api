using System;

namespace Weixin.Api.Entity
{
    /// <summary>
    /// 打开微信状态通知
    /// </summary>
    public class OpenWxStatusNotifyRequest : WeixinRequest<OpenWxStatusNotifyResponse>
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
                var request = new _OpenWxStatusNotifyRequest
                {
                    BaseRequest = new WxBaseRequest(this.WxRequest),
                    ClientMsgId = Common.ExDateTime.Timestamp,
                    Code = 3,
                    FromUserName = this.UserName,
                    ToUserName = this.UserName
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

        public OpenWxStatusNotifyRequest()
            : base()
        {
            this.Referer = "https://wx2.qq.com/";
            this.Origin = "https://wx2.qq.com/";
            this.KeepAlive = true;
            this.HttpMethod = Enum.HttpMethods.POST;
            this.Host = "wx2.qq.com";
        }

        public WxRequest WxRequest { get; set; }

        public string UserName { get; set; }

        public OpenWxStatusNotifyRequest(WxRequest request, string userName)
            : this()
        {
            this.WxRequest = request;
            this.UserName = userName;
        }

        public class _OpenWxStatusNotifyRequest
        {
            public WxBaseRequest BaseRequest { get; set; }
            public long ClientMsgId { get; set; }
            public int Code { get; set; }
            public string FromUserName { get; set; }
            public string ToUserName { get; set; }
        }
    }
}
using System;

namespace Weixin.Api.Entity
{
    public class WxSyncRequest : WeixinRequest<WxSyncResponse>
    {
        /// <summary>
        /// IWeixinClient会自动设置Host，手工设置的值会被覆盖
        /// </summary>
        public string Host { get; set; }

        public string SId { get; set; }

        public string SKey { get; set; }

        public string Lang { get; set; }

        public string PassTicket { get; set; }

        public override string ApiUrl
        {
            get
            {
                return string.Format("https://{0}/cgi-bin/mmwebwx-bin/webwxsync?sid={1}&skey={2}&lang={3}&pass_ticket={4}",
                    this.Host,
                    this.SId,
                    this.SKey,
                    this.Lang,
                    this.PassTicket);
            }
        }

        public override string QueryString
        {
            get
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(this.Request);
            }
        }

        public override string Accept
        {
            get
            {
                return "application/json, text/plain, */*";
            }
        }

        public WxSyncRequest()
            : base()
        {
            this.Host = "wx2.qq.com";
            this.KeepAlive = true;
            this.Origin = "https://wx.qq.com";
            this.Referer = "https://wx.qq.com/?lang=zh_CN";
            this.HttpMethod = Enum.HttpMethods.POST;
            this.ContentType = Enum.ContentTypes.JSON;
            this.Lang = "zh_CN";
        }

        public WxSyncRequest(WxRequest request, string passTicket, WxSyncKey syncKey,
            string sid, string skey, string lang = "zh_CN")
            : this()
        {
            this.Request = new _WxSyncRequest(request, syncKey);
            this.PassTicket = passTicket;
            this.SId = sid;
            this.SKey = skey;
            this.Lang = lang;
        }

        public _WxSyncRequest Request { get; set; }

        public class _WxSyncRequest
        {
            public WxBaseRequest BaseRequest { get; set; }

            [Newtonsoft.Json.JsonProperty("rr")]
            public long RR
            {
                get
                {
                    return -Common.ExDateTime.Timestamp;
                }
            }

            public WxSyncKey SyncKey { get; set; }

            public _WxSyncRequest()
            {

            }

            public _WxSyncRequest(WxRequest request, WxSyncKey syncKey)
                : this()
            {
                this.BaseRequest = new WxBaseRequest(request);
                this.SyncKey = syncKey;
            }
        }
    }
}
using System;

namespace Weixin.Api.Entity
{
    public class SyncCheckRequest : WeixinRequest<SyncCheckResponse>
    {
        /// <summary>
        /// IWeixinClient会自动设置Host，手工设置的值会被覆盖
        /// </summary>
        public string Host { get; set; }

        public string SKey { get; set; }

        public string SId { get; set; }

        public string Uin { get; set; }

        public string DeviceId { get; set; }

        public string SyncKey
        {
            get
            {
                if (this.Keys == null)
                    return "";
                var s = "";
                foreach (var key in Keys)
                {
                    s += string.Format("{0}_{1}|", key.Key, key.Val);
                }
                if (!string.IsNullOrWhiteSpace(s))
                    return s.TrimEnd('|');
                return "";
            }
        }

        public WxList[] Keys { get; set; }

        public override string ApiUrl
        {
            get
            {
                return string.Format("https://webpush.{0}/cgi-bin/mmwebwx-bin/synccheck?r={1}&skey={2}&sid={3}&uin={4}&deviceid={5}&synckey={6}&_={7}",
                    this.Host,
                    Common.ExDateTime.Timestamp,
                    System.Web.HttpUtility.UrlEncode(this.SKey),
                    this.SId,
                    this.Uin,
                    this.DeviceId,
                    System.Web.HttpUtility.UrlEncode(this.SyncKey),
                    Common.ExDateTime.Timestamp);
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
                return "*/*";
            }
        }

        public SyncCheckRequest()
            : base()
        {
            this.Host = "wx2.qq.com";
            this.Referer = "https://wx.qq.com/?lang=zh_CN";
            this.KeepAlive = true;
            this.HttpMethod = Enum.HttpMethods.GET;
            this.ContentType = Enum.ContentTypes.NONE;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="syncKeyList">初始化的时候返回的SyncKey</param>
        public SyncCheckRequest(WxRequest request, WxList[] syncKeyList)
            : this()
        {
            this.SKey = request.Skey;
            this.SId = request.Sid;
            this.DeviceId = request.DeviceID;
            this.Uin = request.Uin;
            this.Keys = syncKeyList;
        }
    }
}
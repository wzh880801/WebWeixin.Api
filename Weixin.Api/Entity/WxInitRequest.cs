using System;

namespace Weixin.Api.Entity
{
    /// <summary>
    /// 获取初始化信息（账号头像信息、聊天好友、阅读等）
    /// </summary>
    public class WxInitRequest : WeixinRequest<WxInitResponse>
    {
        public override string Accept
        {
            get
            {
                return "application/json, text/plain, */*";
            }
        }

        public override string QueryString
        {
            get
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(this.BaseRequest);
            }
        }

        public override string ApiUrl
        {
            get
            {
                return string.Format("https://{0}/cgi-bin/mmwebwx-bin/webwxinit?r=-{1}",
                    this.Host,
                    Common.ExDateTime.Timestamp);
            }
        }

        public WxBaseRequest BaseRequest { get; set; }

        /// <summary>
        /// IWeixinClient会自动设置Host，手工设置的值会被覆盖
        /// </summary>
        public string Host { get; set; }

        public WxInitRequest()
            : base()
        {
            this.KeepAlive = true;
            this.Referer = "https://wx.qq.com/";
            this.ContentType = Enum.ContentTypes.JSON;
            this.HttpMethod = Enum.HttpMethods.POST;
            this.Host = "wx2.qq.com";
        }

        public WxInitRequest(WxRequest request)
            : this()
        {
            this.BaseRequest = new WxBaseRequest(request);
        }
    }
}
using System;

namespace Weixin.Api.Entity
{
    public class GetMsgImgRequest : WeixinRequest<GetMsgImgResponse>
    {
        public override string ApiUrl
        {
            get
            {
                return string.Format("https://{0}/cgi-bin/mmwebwx-bin/webwxgetmsgimg?&MsgID={1}&skey={2}&{3}={4}",
                    this.Host,
                    this.MsgId,
                    this.SKey,
                    this.FunType,
                    this.FunValue);
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
                return "image/webp,image/*,*/*;q=0.8";
            }
        }

        public string Host { get; set; }
        public string MsgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FunValue
        {
            get
            {
                if (this.FunType == "fun")
                    return "download";
                else if (this.FunType == "type")
                    return "slave";

                return "";
            }
        }

        /// <summary>
        /// fun/type/_
        /// </summary>
        public string FunType { get; set; }
        public string SKey { get; set; }

        public GetMsgImgRequest()
            : base()
        {
            this.Host = "wx2.qq.com";
            this.KeepAlive = true;
            this.Referer = "https://wx.qq.com/?lang=zh_CN";
            this.ContentType = Enum.ContentTypes.NONE;
        }

        public GetMsgImgRequest(string msgId, string skey, string funType = "type")
            : this()
        {
            this.SKey = skey;
            this.MsgId = msgId;
            this.FunType = funType;
        }
    }
}
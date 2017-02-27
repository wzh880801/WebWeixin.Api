using System;

namespace Weixin.Api.Entity
{
    /// <summary>
    /// 获取联系人列表，如果要获取群组信息，请使用<seealso cref="BatchGetContactRequest"/>
    /// </summary>
    public class GetContactRequest : WeixinRequest<GetContactResponse>
    {
        public override string ApiUrl
        {
            get
            {
                return string.Format("https://{0}/cgi-bin/mmwebwx-bin/webwxgetcontact?r={1}&seq={2}&skey={3}",
                    this.Host,
                    Common.ExDateTime.Timestamp,
                    this.Seq,
                    this.Skey);
            }
        }

        /// <summary>
        /// IWeixinClient会自动设置Host，手工设置的值会被覆盖
        /// </summary>
        public string Host { get; set; }

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
                return "";
            }
        }

        public string Skey { get; set; }
        public int Seq { get; set; }

        public GetContactRequest()
            : base()
        {
            this.KeepAlive = true;
            this.HttpMethod = Enum.HttpMethods.GET;
            this.Referer = "https://wx2.qq.com/";
            this.Host = "wx2.qq.com";
        }

        public GetContactRequest(string skey, int seq = 0)
            : this()
        {
            this.Skey = skey;
            this.Seq = seq;
        }
    }
}
using System;

namespace Weixin.Api.Entity
{
    /// <summary>
    /// 获取群组信息，如果要获取联系人信息，请使用<seealso cref="GetContactRequest"/>
    /// <para>第一次调取只会返回最近活跃的群组</para>
    /// <para>第二次调用会返回剩下的群组</para>
    /// </summary>
    public class BatchGetContactRequest : WeixinRequest<BatchGetContactResponse>
    {
        /// <summary>
        /// IWeixinClient会自动设置Host，手工设置的值会被覆盖
        /// </summary>
        public string Host { get; set; }

        public string PassTicket { get; set; }

        public WxRequest WxRequest { get; set; }

        public _BatchGetContactRequestItem[] List { get; set; }

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
                var request = new _BatchGetContactRequest(new WxBaseRequest(this.WxRequest), this.List);
                return Newtonsoft.Json.JsonConvert.SerializeObject(request);
            }
        }

        public override string ApiUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.PassTicket))
                    throw new ArgumentNullException(nameof(PassTicket));

                return string.Format("https://{0}/cgi-bin/mmwebwx-bin/webwxbatchgetcontact?type=ex&r={1}&lang=zh_CN&pass_ticket={2}",
                    this.Host,
                    Common.ExDateTime.Timestamp,
                    this.PassTicket);
            }
        }

        public BatchGetContactRequest()
            : base()
        {
            this.Host = "wx2.qq.com";
            this.KeepAlive = true;
            this.Referer = "https://wx.qq.com/?lang=zh_CN";
            this.Origin = "https://wx.qq.com/";
            this.ContentType = Enum.ContentTypes.JSON;
            this.HttpMethod = Enum.HttpMethods.POST;
        }

        public BatchGetContactRequest(
            WxRequest request,
            _BatchGetContactRequestItem[] list,
            string passTicket)
            : this()
        {
            this.WxRequest = request;
            this.List = list;
            this.PassTicket = passTicket;
        }

        public class _BatchGetContactRequest
        {
            public WxBaseRequest BaseRequest { get; set; }
            public int Count
            {
                get
                {
                    if (this.List == null)
                        return 0;

                    return this.List.Length;
                }
            }
            public _BatchGetContactRequestItem[] List { get; set; }

            public _BatchGetContactRequest()
            {

            }

            public _BatchGetContactRequest(WxBaseRequest request, _BatchGetContactRequestItem[] list)
                : this()
            {
                this.BaseRequest = request;
                this.List = list;
            }
        }

        public class _BatchGetContactRequestItem
        {
            public string EncryChatRoomId { get; set; }
            public string UserName { get; set; }

            public _BatchGetContactRequestItem()
            {
                this.EncryChatRoomId = "";
            }
        }
    }
}
using System;

namespace Weixin.Api.Entity
{
    public class ChangeRemarkNameRequest : WeixinRequest<ChangeRemarkNameResponse>
    {
        public WxBaseRequest BaseRequest { get; set; }

        /// <summary>
        /// 命令Id
        /// <para>2 - 修改备注名称</para>
        /// </summary>
        public long CmdId { get; set; }

        /// <summary>
        /// 新的备注名称
        /// </summary>
        public string RemarkName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        public override string QueryString
        {
            get
            {
                var msg = new _ChangeRemarkNameRequest
                {
                    BaseRequest = this.BaseRequest,
                    UserName = this.UserName,
                    CmdId = this.CmdId,
                    RemarkName = this.RemarkName
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(msg);
            }
        }

        public override string ApiUrl
        {
            get
            {
                return string.Format("https://{0}/cgi-bin/mmwebwx-bin/webwxoplog", this.Host);
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

        public class _ChangeRemarkNameRequest
        {
            public WxBaseRequest BaseRequest { get; set; }

            /// <summary>
            /// 命令Id
            /// <para>2 - 修改备注名称</para>
            /// </summary>
            public long CmdId { get; set; }

            /// <summary>
            /// 新的备注名称
            /// </summary>
            public string RemarkName { get; set; }

            /// <summary>
            /// 用户名
            /// </summary>
            public string UserName { get; set; }
        }

        public ChangeRemarkNameRequest()
            : base()
        {
            this.Host = "wx2.qq.com";
            this.Referer = "https://wx2.qq.com/?&lang=zh_CN";
            this.Origin = "https://wx2.qq.com";
            this.HttpMethod = Enum.HttpMethods.POST;
            this.ContentType = Enum.ContentTypes.JSON;
            this.KeepAlive = true;
        }

        public ChangeRemarkNameRequest(string userName, string remarkName, long cmdId = 2)
            : this()
        {
            this.CmdId = cmdId;
            this.UserName = userName;
            this.RemarkName = remarkName;
        }
    }
}
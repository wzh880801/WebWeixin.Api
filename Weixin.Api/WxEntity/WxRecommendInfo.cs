using System;

namespace Weixin.Api.Entity
{
    public class WxRecommendInfo
    {
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        public long QQNum { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        /// <summary>
        /// 打招呼内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 微信签名
        /// </summary>
        public string Signature { get; set; }
        public string Alias { get; set; }
        public int Scene { get; set; }
        public int VerifyFlag { get; set; }
        public long AttrStatus { get; set; }
        public int Sex { get; set; }
        public string Ticket { get; set; }
        public int OpCode { get; set; }
    }
}
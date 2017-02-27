using System;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    public class BatchGetContactResponse : WeixinResponse
    {
        public override ResponseType ResponseType
        {
            get
            {
                return ResponseType.JSON;
            }
        }

        public WxBaseResponse BaseResponse { get; set; }

        public WxContact[] ContactList { get; set; }

        public int Count { get; set; }
    }
}
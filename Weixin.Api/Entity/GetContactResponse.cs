using System;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    public class GetContactResponse : WeixinResponse
    {
        public override ResponseType ResponseType
        {
            get
            {
                return ResponseType.JSON;
            }
        }

        public WxBaseResponse BaseResponse { get; set; }

        public int MemberCount { get; set; }

        public WxContact[] MemberList { get; set; }

        public int Sep { get; set; }
    }
}
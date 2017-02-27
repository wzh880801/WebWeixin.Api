using System;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    public class VerifyUserResponse : WeixinResponse
    {
        public WxBaseResponse BaseResponse { get; set; }

        public override ResponseType ResponseType
        {
            get
            {
                return ResponseType.JSON;
            }
        }
    }
}
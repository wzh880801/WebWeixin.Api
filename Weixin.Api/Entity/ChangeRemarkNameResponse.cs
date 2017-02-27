using System;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    public class ChangeRemarkNameResponse : WeixinResponse
    {
        public override ResponseType ResponseType
        {
            get
            {
                return ResponseType.JSON;
            }
        }

        public WxBaseResponse BaseResponse { get; set; }
    }
}
using System;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    public class WxStatusNotifyResponse : WeixinResponse
    {
        public override ResponseType ResponseType
        {
            get
            {
                return ResponseType.JSON;
            }
        }

        public WxBaseResponse BaseResponse { get; set; }

        public string MsgID { get; set; }
    }
}
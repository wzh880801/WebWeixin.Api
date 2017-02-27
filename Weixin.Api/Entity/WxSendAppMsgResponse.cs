using System;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    public class WxSendAppMsgResponse : WeixinResponse
    {
        public override ResponseType ResponseType
        {
            get
            {
                return ResponseType.JSON;
            }
        }

        public WxBaseResponse BaseResponse { get; set; }

        public string LocalID { get; set; }

        public string MsgID { get; set; }
    }
}
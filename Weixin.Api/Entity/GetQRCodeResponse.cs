using System;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    public class GetQRCodeResponse : WeixinResponse
    {
        [Common.Regex("QRCode", @"window.QRLogin.uuid = ""(?<QRCode>.+?)"";")]
        public string QRLoginUUId { get; set; }

        [Common.Regex("Code", @"window.QRLogin.code = (?<Code>.+?);")]
        public int QRLoginCode { get; set; }

        public override ResponseType ResponseType
        {
            get
            {
                return ResponseType.JavaScript;
            }
        }
    }
}
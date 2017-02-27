using System;
using System.IO;
using System.Drawing;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    public class GetQRImageResponse : WeixinResponse
    {
        public Image QRImage
        {
            get
            {
                return Helper.ImageHelper.FromBase64String(this.ResponseBase64String);
            }
        }

        public override ResponseType ResponseType
        {
            get
            {
                return Enum.ResponseType.Stream;
            }
        }
    }
}
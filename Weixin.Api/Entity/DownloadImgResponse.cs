using System;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    public class DownloadImgResponse : WeixinResponse
    {
        public override ResponseType ResponseType
        {
            get
            {
                return ResponseType.Stream;
            }
        }

        public System.Drawing.Image HeadImage
        {
            get
            {
                return Helper.ImageHelper.FromBase64String(this.ResponseBase64String);
            }
        }
    }
}
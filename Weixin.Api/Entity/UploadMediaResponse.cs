using System;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    public class UploadMediaResponse : WeixinResponse
    {
        public override ResponseType ResponseType
        {
            get
            {
                return ResponseType.JSON;
            }
        }

        public WxBaseResponse BaseResponse { get; set; }
        public string MediaId { get; set; }
        public int StartPos { get; set; }
        public int CDNThumbImgHeight { get; set; }
        public int CDNThumbImgWidth { get; set; }
    }
}
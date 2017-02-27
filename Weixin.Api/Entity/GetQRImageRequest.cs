using System;

namespace Weixin.Api.Entity
{
    public class GetQRImageRequest : WeixinRequest<GetQRImageResponse>
    {
        public string QRCode { get; set; }

        public GetQRImageRequest()
            : base()
        {
            this.Referer = "https://wx.qq.com/";
        }

        public GetQRImageRequest(string code)
        {
            this.QRCode = code;
            this.Referer = "https://wx.qq.com/";
        }

        public override string ApiUrl
        {
            get
            {
                return string.Format("https://login.weixin.qq.com/qrcode/{0}", this.QRCode);
            }
        }

        public override string QueryString
        {
            get
            {
                return null;
            }
        }

        public override string Accept
        {
            get
            {
                return "image/png, image/svg+xml, image/jxr, image/*;q=0.8, */*;q=0.5";
            }
        }
    }
}
using System;

namespace Weixin.Api.Entity
{
    public class DownloadImgRequest : WeixinRequest<DownloadImgResponse>
    {
        public override string ApiUrl
        {
            get
            {
                return string.Format("https://{0}{1}",
                    this.Host,
                    this.ImagePath);
            }
        }

        public override string QueryString
        {
            get
            {
                return "";
            }
        }

        public override string Accept
        {
            get
            {
                return "image/webp,image/*,*/*;q=0.8";
            }
        }

        /// <summary>
        /// IWeixinClient会自动设置Host，手工设置的值会被覆盖
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 图片路径
        /// <para>不带Host的路径</para>
        /// </summary>
        public string ImagePath { get; set; }

        public DownloadImgRequest()
            : base()
        {
            this.Host = "wx2.qq.com";
            this.KeepAlive = true;
            this.Referer = "https://wx.qq.com/?lang=zh_CN";
            this.ContentType = Enum.ContentTypes.NONE;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagePath"></param>
        public DownloadImgRequest(string imagePath)
            : this()
        {
            this.ImagePath = imagePath;
        }
    }
}
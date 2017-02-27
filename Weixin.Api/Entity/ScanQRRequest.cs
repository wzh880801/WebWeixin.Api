using System;

namespace Weixin.Api.Entity
{
    public class ScanQRRequest : WeixinRequest<ScanQRResponse>
    {
        public override string ApiUrl
        {
            get
            {
                return string.Format("https://login.wx2.qq.com/cgi-bin/mmwebwx-bin/login?loginicon=true&uuid={0}&tip={1}&r=-{2}&_={3}",
                    this.UUId,
                    this.Tip,
                    new Random(Guid.NewGuid().GetHashCode()).Next(334203100, 334299999),
                    Common.ExDateTime.Timestamp);
            }
        }

        public override string QueryString
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string Accept
        {
            get
            {
                return "application/javascript, */*;q=0.8";
            }
        }

        /// <summary>
        /// 0-已扫描 1-未扫描
        /// </summary>
        public int Tip { get; set; }

        /// <summary>
        /// QRCode
        /// </summary>
        public string UUId { get; set; }

        public ScanQRRequest(int tip, string uuid)
            : this()
        {
            this.Tip = tip;
            this.UUId = uuid;
        }

        public ScanQRRequest()
            : base()
        {
            this.ContentType = Enum.ContentTypes.NONE;
            this.KeepAlive = true;
        }
    }
}
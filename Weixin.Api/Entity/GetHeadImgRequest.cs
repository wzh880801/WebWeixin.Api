//using System;

//namespace Weixin.Api.Entity
//{
//    public class GetHeadImgRequest : WeixinRequest<GetHeadImgResponse>
//    {
//        public override string ApiUrl
//        {
//            get
//            {
//                return string.Format("https://{0}/cgi-bin/mmwebwx-bin/webwxgetheadimg?seq={1}&username={2}&skey={3}",
//                    this.Host,
//                    this.Seq,
//                    this.GroupUserName,
//                    this.SKey);
//            }
//        }

//        public override string QueryString
//        {
//            get
//            {
//                return "";
//            }
//        }

//        public override string Accept
//        {
//            get
//            {
//                return "image/webp,image/*,*/*;q=0.8";
//            }
//        }

//        public string Host { get; set; }
//        public int Seq { get; set; }
//        public string GroupUserName { get; set; }
//        public string SKey { get; set; }

//        public GetHeadImgRequest()
//        {
//            this.Host = "wx2.qq.com";
//            this.KeepAlive = true;
//            this.Referer = "https://wx.qq.com/?lang=zh_CN";
//            this.ContentType = Enum.ContentTypes.NONE;
//        }

//        public GetHeadImgRequest(string groupUserName, string host = "wx2.qq.com", string skey = "", int seq = 0)
//            : this()
//        {
//            this.Host = host;
//            this.SKey = skey;
//            this.GroupUserName = groupUserName;
//            this.Seq = seq;
//        }
//    }
//}
//using System;

//namespace Weixin.Api.Entity
//{
//    public class GetIconRequest : WeixinRequest<GetIconResponse>
//    {
//        public string Host { get; set; }

//        public int Seq { get; set; }

//        public string UserName { get; set; }

//        public string SKey { get; set; }

//        public override string ApiUrl
//        {
//            get
//            {
//                return string.Format("https://{0}/cgi-bin/mmwebwx-bin/webwxgeticon?seq={1}&username={2}&skey={3}",
//                    this.Host,
//                    this.Seq,
//                    this.UserName,
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

//        public GetIconRequest()
//        {
//            this.Host = "wx2.qq.com";
//            this.Referer = "https://wx.qq.com/?lang=zh_CN";
//            this.Seq = 0;
//            this.KeepAlive = true;
//        }

//        public GetIconRequest(string userName, string skey, string host = "wx2.qq.com", int seq = 0)
//            : this()
//        {
//            this.UserName = userName;
//            this.SKey = skey;
//            this.Host = host;
//            this.Seq = seq;
//        }
//    }
//}
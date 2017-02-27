//using System;
//using System.IO;
//using System.Drawing;
//using Weixin.Api.Enum;

//namespace Weixin.Api.Entity
//{
//    public class GetIconResponse : WeixinResponse
//    {
//        public override ResponseType ResponseType
//        {
//            get
//            {
//                return ResponseType.Stream;
//            }
//        }

//        public System.Drawing.Image Image
//        {
//            get
//            {
//                return Helper.ImageHelper.FromBase64String(this.ResponseBase64String);
//            }
//        }
//    }
//}
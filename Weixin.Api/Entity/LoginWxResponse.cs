using System;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    [System.Xml.Serialization.XmlRootAttribute("error", Namespace = "", IsNullable = false)]
    public class LoginWxResponse : WeixinResponse
    {
        public override ResponseType ResponseType
        {
            get
            {
                return ResponseType.XML;
            }
        }

        [System.Xml.Serialization.XmlElement("ret")]
        public int Ret { get; set; }

        [System.Xml.Serialization.XmlElement("message")]
        public object Message { get; set; }

        [System.Xml.Serialization.XmlElement("skey")]
        public string SKey { get; set; }

        [System.Xml.Serialization.XmlElement("wxsid")]
        public string WxSid { get; set; }

        [System.Xml.Serialization.XmlElement("wxuin")]
        public string WxUin { get; set; }

        [System.Xml.Serialization.XmlElement("pass_ticket")]
        public string PassTicket { get; set; }

        [System.Xml.Serialization.XmlElement("isgrayscale")]
        public int IsGrayScale { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public string DecodedPassTicket
        {
            get
            {
                return System.Web.HttpUtility.UrlDecode(this.PassTicket);
            }
        }
    }
}
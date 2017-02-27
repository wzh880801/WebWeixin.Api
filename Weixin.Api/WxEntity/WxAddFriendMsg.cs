using System;

namespace Weixin.Api.Entity
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("msg", Namespace = "", IsNullable = false)]
    public class WxAddFriendMsg
    {
        [System.Xml.Serialization.XmlElement("brandlist")]
        public MsgBrandList BrandList { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("fromusername")]
        public string FromUserName { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("encryptusername")]
        public string EncryptUserName { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("fromnickname")]
        public string FromNickName { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("content")]
        public string Content { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("shortpy")]
        public string ShortPY { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("imagestatus")]
        public long ImageStatus { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("scene")]
        public int Scene { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("country")]
        public string Country { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("province")]
        public string Province { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("city")]
        public string City { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("sign")]
        public string Sign { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("percard")]
        public int PerCard { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("sex")]
        public int Sex { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("alias")]
        public string Alias { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("weibo")]
        public string WeiBo { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("weibonickname")]
        public string WeiBoNickName { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("albumflag")]
        public int AlbumFlag { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("albumstyle")]
        public int AlbumStyle { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("albumbgimgid")]
        public string AlbumBgImgId { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("snsflag")]
        public int SnsFlag { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("snsbgimgid")]
        public string SnsBgImgId { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("snsbgobjectid")]
        public int SnsBgObjectId { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("mhash")]
        public string MHash { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("mfullhash")]
        public string MfullHash { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("bigheadimgurl")]
        public string BigHeadImgUrl { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("smallheadimgurl")]
        public string SmallHeadImgUrl { get; set; }

        /// <summary>
        /// 通过验证的时候需要
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("ticket")]
        public string Ticket { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("opcode")]
        public int OpCode { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("googlecontact")]
        public string GoogleContact { get; set; }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("qrticket")]
        public string QRTicket { get; set; }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("chatroomusername")]
        public string ChatroomUserName { get; set; }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("sourceusername")]
        public string SourceUserName { get; set; }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("sourcenickname")]
        public string SourceNickName { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class MsgBrandList
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("count")]
        public int Count { get; set; }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("ver")]
        public long Ver { get; set; }
    }
}
using System;

namespace Weixin.Api.Entity
{
    /// <summary>
    /// 反序列化之前先<seealso cref="System.Web.HttpUtility.HtmlDecode(string)"/>,然后去掉其中的&lt;br/&gt;
    /// </summary>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("msg", Namespace = "", IsNullable = false)]
    public class WxImgMsgContent
    {
        [System.Xml.Serialization.XmlElement("img")]
        public MsgImgContent ImgContent { get; set; }

        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public class MsgImgContent
        {

            [System.Xml.Serialization.XmlAttributeAttribute("aeskey")]
            public string AESKey { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("encryver")]
            public string EncryVersion { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("cdnthumbaeskey")]
            public string CdnThumbAESKey { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("cdnthumburl")]
            public string CdnThumbUrl { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("cdnthumblength")]
            public long CdnThumbLength { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("cdnthumbheight")]
            public int CdnThumbHeight { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("cdnthumbwidth")]
            public int CdnThumbWidth { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("cdnmidheight")]
            public int CdnMidHeight { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("cdnmidwidth")]
            public int CdnMidWidth { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("cdnhdheight")]
            public int CdnHDHeight { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("cdnhdwidth")]
            public int CdnHDWidth { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("cdnmidimgurl")]
            public string CdnMidImgUrl { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("length")]
            public long Length { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("cdnbigimgurl")]
            public string CdnBigImgUrl { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("hdlength")]
            public int HDLength { get; set; }


            [System.Xml.Serialization.XmlAttributeAttribute("md5")]
            public string MD5 { get; set; }
        }
    }
}
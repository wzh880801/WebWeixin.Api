using System;

namespace Weixin.Api.Entity
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("msg", Namespace = "", IsNullable = false)]
    public class WxAppMsgContent
    {
        [System.Xml.Serialization.XmlElement("appmsg")]
        public AppMsgContent AppMsg { get; set; }

        [System.Xml.Serialization.XmlElement("fromusername")]
        public string FromUserName { get; set; }

        [System.Xml.Serialization.XmlElement("scene")]
        public long Scene { get; set; }

        [System.Xml.Serialization.XmlElement("appinfo")]
        public AppInfo AppInformation { get; set; }

        [System.Xml.Serialization.XmlElement("commenturl")]
        public string CommentUrl { get; set; }

        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public class AppMsgContent
        {
            [System.Xml.Serialization.XmlElement("title")]
            public string Title { get; set; }

            [System.Xml.Serialization.XmlElement("des")]
            public string Description { get; set; }

            [System.Xml.Serialization.XmlElement("action")]
            public string Action { get; set; }

            [System.Xml.Serialization.XmlElement("type")]
            public int Type { get; set; }

            [System.Xml.Serialization.XmlElement("showtype")]
            public int ShowType { get; set; }

            [System.Xml.Serialization.XmlElement("content")]
            public string Content { get; set; }

            [System.Xml.Serialization.XmlElement("url")]
            public string Url { get; set; }

            [System.Xml.Serialization.XmlElement("dataurl")]
            public string DataUrl { get; set; }

            [System.Xml.Serialization.XmlElement("contentattr")]
            public string ContentAttr { get; set; }

            [System.Xml.Serialization.XmlElement("lowurl")]
            public string LowUrl { get; set; }

            [System.Xml.Serialization.XmlElement("lowdataurl")]
            public string LowDataUrl { get; set; }

            [System.Xml.Serialization.XmlElement("recorditem")]
            public string RecordItem { get; set; }

            [System.Xml.Serialization.XmlElement("thumburl")]
            public string ThumbUrl { get; set; }

            [System.Xml.Serialization.XmlElement("extinfo")]
            public string ExtInfo { get; set; }

            [System.Xml.Serialization.XmlElement("sourceusername")]
            public string SourceUserName { get; set; }

            [System.Xml.Serialization.XmlElement("sourcedisplayname")]
            public string SourceDisplayName { get; set; }

            [System.Xml.Serialization.XmlElement("commenturl")]
            public string CommentUrl { get; set; }

            [System.Xml.Serialization.XmlElement("appattach")]
            public AppAttach AppAttach { get; set; }

            [System.Xml.Serialization.XmlAttributeAttribute("appid")]
            public string Appid { get; set; }

            [System.Xml.Serialization.XmlAttributeAttribute("sdkver")]
            public string SdkVersion { get; set; }
        }

        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public class AppAttach
        {
            [System.Xml.Serialization.XmlElement("totallen")]
            public long TotalLength { get; set; }

            [System.Xml.Serialization.XmlElement("attachid")]
            public string AttachId { get; set; }

            [System.Xml.Serialization.XmlElement("emoticonmd5")]
            public string EmoticonMD5 { get; set; }

            [System.Xml.Serialization.XmlElement("fileext")]
            public string FileExtension { get; set; }

            [System.Xml.Serialization.XmlElement("cdnthumburl")]
            public string cdnthumburl { get; set; }

            [System.Xml.Serialization.XmlElement("cdnthumblength")]
            public long CdnThumbLength { get; set; }

            [System.Xml.Serialization.XmlElement("cdnthumbheight")]
            public int CdnThumbHeight { get; set; }

            [System.Xml.Serialization.XmlElement("cdnthumbwidth")]
            public int CdnThumbWidth { get; set; }

            [System.Xml.Serialization.XmlElement("aeskey")]
            public string AESKey { get; set; }

            [System.Xml.Serialization.XmlElement("cdnthumbaeskey")]
            public string CdnThumbAESkey { get; set; }

            [System.Xml.Serialization.XmlElement("encryver")]
            public string EncryVersion { get; set; }
        }

        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public class AppInfo
        {
            [System.Xml.Serialization.XmlElement("version")]
            public string Version { get; set; }

            [System.Xml.Serialization.XmlElement("appname")]
            public string AppName { get; set; }
        }
    }
}
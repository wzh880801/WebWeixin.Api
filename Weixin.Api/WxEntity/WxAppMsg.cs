using System;

namespace Weixin.Api.Entity
{
    /// <summary>
    /// 应用消息，比如从微信Web端发来的消息
    /// </summary>
    public class WxAppMsg
    {
        /// <summary>
        /// 消息类型
        /// <para>6 App消息（先把文件上传得到MediaId再调用webwxsendappmsg发送）</para>
        /// </summary>
        public int Type { get { return 6; } }

        /// <summary>
        /// 要发送的消息（发送图片消息时该字段为MediaId）
        /// </summary>
        public string Content
        {
            get
            {
                return string.Format("<appmsg appid='wxeb7ec651dd0aefa9' sdkver=''><title>{0}</title><des></des><action></action><type>6</type><content></content><url></url><lowurl></lowurl><appattach><totallen>{1}</totallen><attachid>{2}</attachid><fileext>{3}</fileext></appattach><extinfo></extinfo></appmsg>",
                    this.Title,
                    this.Size,
                    this.MediaId,
                    this.FileExtension);
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public string FileExtension { get; set; }

        public WxAppMsg()
        {

        }

        public WxAppMsg(System.IO.FileInfo file, UploadMediaResponse response, string fromUserName, string toUserName)
            : this()
        {
            this.MediaId = response.MediaId;
            this.Size = response.StartPos;
            this.FileExtension = file.Extension.ToString().TrimStart('.');
            this.Title = file.Name;
            this.FromUserName = fromUserName;
            this.ToUserName = toUserName;
        }

        [Newtonsoft.Json.JsonIgnore]
        public string Title { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public long Size { get; set; }

        /// <summary>
        /// 自己的ID
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 目标ID，可以是微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 与ClientMsgId一样
        /// </summary>
        public string LocalID
        {
            get
            {
                return this.ClientMsgId;
            }
        }

        /// <summary>
        /// 时间戳左移4位随后补上4位随机数
        /// </summary>
        public string ClientMsgId
        {
            get
            {
                var r = new Random(Guid.NewGuid().GetHashCode()).Next(1, 10000);
                return string.Format("{0}{1:0000}", Common.ExDateTime.Timestamp, r);
            }
        }

        public string MediaId { get; set; }
    }
}
using System;

namespace Weixin.Api.Entity
{
    /// <summary>
    /// 图片消息
    /// </summary>
    public class WxImgMsg
    {
        /// <summary>
        /// 消息类型
        /// <para>3 图片消息（先把图片上传得到MediaId再调用webwxsendmsg发送）</para>
        /// </summary>
        public int Type { get { return 3; } }

        /// <summary>
        /// 要发送的消息（发送图片消息时该字段为MediaId）
        /// </summary>
        public string Content { get { return ""; } }

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
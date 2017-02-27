using System;

namespace Weixin.Api.Entity
{
    public class WxAddMsg
    {
        public string MsgId { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }

        /// <summary>
        /// <para>1 - 文本信息|位置信息|新闻信息</para>
        /// <para>3 - 图片信息</para>
        /// <para>6 - App信息</para>
        /// <para>34 - 语音消息</para>
        /// <para>37 - 朋友添加信息<seealso cref="WxAddFriendMsg"/></para>
        /// <para>42 - 个人名片</para>
        /// <para>43 - 小视频</para>
        /// <para>47 - 非官方表情</para>
        /// <para>49 - 转发链接  AppMsgType = 5 | 转账 FileName=微信转账 </para>
        /// <para>10000 - 红包 | AppMsgType = 0 | 朋友验证通过 Content=你已添加了Jarvis，现在可以开始聊天了。</para>
        /// </summary>
        public int MsgType { get; set; }

        /// <summary>
        /// 消息内容
        /// <para>MsgType=37时可 <seealso cref="System.Web.HttpUtility.HtmlDecode(string)"/> 然后反序列化为<seealso cref="WxAddFriendMsg"/></para>
        /// </summary>
        public string Content { get; set; }
        public int Status { get; set; }
        public int ImgStatus { get; set; }
        public int CreateTime { get; set; }
        public int VoiceLength { get; set; }
        public int PlayLength { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string MediaId { get; set; }
        public string Url { get; set; }
        public int AppMsgType { get; set; }
        public int StatusNotifyCode { get; set; }
        public string StatusNotifyUserName { get; set; }
        public WxRecommendInfo RecommendInfo { get; set; }
        public int ForwardFlag { get; set; }
        public WxAppInfo AppInfo { get; set; }
        public int HasProductId { get; set; }
        public string Ticket { get; set; }
        public int ImgHeight { get; set; }
        public int ImgWidth { get; set; }
        public int SubMsgType { get; set; }
        public long NewMsgId { get; set; }
        public string OriContent { get; set; }
    }
}
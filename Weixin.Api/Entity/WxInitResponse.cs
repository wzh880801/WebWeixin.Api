using System;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    /// <summary>
    /// 初始化信息（账号头像信息、聊天好友、阅读等）
    /// <para>初始化只返回最近的活跃对象</para>
    /// </summary>
    public class WxInitResponse : WeixinResponse
    {
        public override ResponseType ResponseType
        {
            get
            {
                return ResponseType.JSON;
            }
        }

        [Newtonsoft.Json.JsonProperty("BaseResponse")]
        public WxBaseResponse BaseResponse { get; set; }
        public int Count { get; set; }
        public WxContact[] ContactList { get; set; }
        public WxSyncKey SyncKey { get; set; }
        public WxUser User { get; set; }
        public string ChatSet { get; set; }
        public string SKey { get; set; }
        public long ClientVersion { get; set; }
        public long SystemTime { get; set; }
        public int GrayScale { get; set; }
        public long InviteStartCount { get; set; }
        public long MPSubscribeMsgCount { get; set; }
        public WxMpSubscribeMsg[] MPSubscribeMsgList { get; set; }
        public int ClickReportInterval { get; set; }

        public string[] GetChatSetArray()
        {
            if (string.IsNullOrWhiteSpace(this.ChatSet))
                return null;
            return this.ChatSet.Split(new char[] { ',' });
        }
    }
}
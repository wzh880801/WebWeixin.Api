using System;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    public class WxSyncResponse : WeixinResponse
    {
        public override ResponseType ResponseType
        {
            get
            {
                return ResponseType.JSON;
            }
        }

        public WxBaseResponse BaseResponse { get; set; }
        public int AddMsgCount { get; set; }
        public WxAddMsg[] AddMsgList { get; set; }
        public int ModContactCount { get; set; }
        public WxContact[] ModContactList { get; set; }
        public int DelContactCount { get; set; }
        public WxDelContact[] DelContactList { get; set; }
        public int ModChatRoomMemberCount { get; set; }
        public WxMember[] ModChatRoomMemberList { get; set; }
        public WxProfile Profile { get; set; }
        public int ContinueFlag { get; set; }
        public WxSyncKey SyncKey { get; set; }
        public string SKey { get; set; }
        public WxSyncKey SyncCheckKey { get; set; }
    }
}
using System;

namespace Weixin.Api.Entity
{
    public class WxProfile
    {
        public int BitFlag { get; set; }
        public WxBuff UserName { get; set; }
        public WxBuff NickName { get; set; }
        public int BindUin { get; set; }
        public WxBuff BindEmail { get; set; }
        public WxBuff BindMobile { get; set; }
        public int Status { get; set; }
        public int Sex { get; set; }
        public int PersonalCard { get; set; }
        public string Alias { get; set; }
        public int HeadImgUpdateFlag { get; set; }
        public string HeadImgUrl { get; set; }
        public string Signature { get; set; }
    }
}
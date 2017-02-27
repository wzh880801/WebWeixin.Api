using System;

namespace Weixin.Api.Entity
{
    public class WxContact
    {
        /// <summary>
        /// 0
        /// </summary>
        public int Uin { get; set; }
        /// <summary>
        /// 1个@是用户 2个@@是群组
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// <para>可能会带有表情，比如</para>
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像图片链接地址，不带host
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 1-好友， 2-群组， 3-公众号
        /// </summary>
        public int ContactFlag { get; set; }
        /// <summary>
        /// 成员数量，只有在群组信息中才有效
        /// </summary>
        public int MemberCount { get; set; }
        /// <summary>
        /// 成员列表
        /// </summary>
        public WxMember[] MemberList { get; set; }
        /// <summary>
        /// 备注名
        /// </summary>
        public string RemarkName { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public int HideInputBarFlag { get; set; }
        /// <summary>
        /// 性别，0-未设置（公众号、保密），1-男，2-女
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 公众号的功能介绍 or 好友的个性签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 0 - 个人账户; 8 - 公众号; 24 - 系统号
        /// </summary>
        public int VerifyFlag { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public int OwnerUin { get; set; }
        /// <summary>
        /// 用户名拼音缩写
        /// </summary>
        public string PYInitial { get; set; }
        /// <summary>
        /// 用户名拼音全拼
        /// </summary>
        public string PYQuanPin { get; set; }
        /// <summary>
        /// 备注拼音缩写
        /// </summary>
        public string RemarkPYInitial { get; set; }
        /// <summary>
        /// 备注拼音全拼
        /// </summary>
        public string RemarkPYQuanPin { get; set; }
        /// <summary>
        /// 是否为星标朋友  0-否  1-是
        /// </summary>
        public int StarFriend { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public int AppAccountFlag { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public int Statues { get; set; }
        /// <summary>
        /// 119911
        /// </summary>
        public long AttrStatus { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Alias { get; set; }
        public int SnsFlag { get; set; }
        public int UniFriend { get; set; }
        public string DisplayName { get; set; }
        public long ChatRoomId { get; set; }
        public string KeyWord { get; set; }
        public string EncryChatRoomId { get; set; }
        public int IsOwner { get; set; }
    }
}
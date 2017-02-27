using System;

namespace Weixin.Api.Entity
{
    public class WxDelContact
    {
        /// <summary>
        /// 1个@是用户 2个@@是群组
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// 1-好友， 2-群组， 3-公众号
        /// <para>删除好友时这里值为0</para>
        /// </summary>
        public int ContactFlag { get; set; }
    }
}
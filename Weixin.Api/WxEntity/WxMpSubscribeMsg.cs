using System;

namespace Weixin.Api.Entity
{
    /// <summary>
    /// 公众号推送的阅读文章
    /// </summary>
    public class WxMpSubscribeMsg
    {
        public string UserName { get; set; }
        public int MPArticleCount { get; set; }
        public WxMpArticle[] MPArticleList { get; set; }
        public long Time { get; set; }
        public string NickName { get; set; }
    }
}
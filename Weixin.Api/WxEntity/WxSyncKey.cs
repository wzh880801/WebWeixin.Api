using System;

namespace Weixin.Api.Entity
{
    public class WxSyncKey
    {
        public int Count { get; set; }
        public WxList[] List { get; set; }
    }
}
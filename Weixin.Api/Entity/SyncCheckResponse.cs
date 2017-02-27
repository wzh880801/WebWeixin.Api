using System;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    public class SyncCheckResponse : WeixinResponse
    {
        public override ResponseType ResponseType
        {
            get
            {
                return ResponseType.JavaScript;
            }
        }

        /// <summary>
        /// <para>0 正常</para>
        /// <para>1100 失败/登出微信 1102</para>
        /// </summary>
        [Common.Regex(Key = "ret", RegexPattern = @"window.synccheck={retcode:""(?<ret>\d+?)"",selector:""(?<selector>\d+?)""}")]
        public int RetCode { get; set; }

        /// <summary>
        /// <para>0 正常</para>
        /// <para>2 新的消息</para>
        /// <para>6 - 朋友验证通过</para>
        /// <para>7 进入/离开聊天界面</para>
        /// </summary>
        [Common.Regex(Key = "selector", RegexPattern = @"window.synccheck={retcode:""(?<ret>\d+?)"",selector:""(?<selector>\d+?)""}")]
        public int Selector { get; set; }
    }
}
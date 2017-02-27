using System;
using Weixin.Api.Enum;

namespace Weixin.Api.Entity
{
    public class ScanQRResponse : WeixinResponse
    {
        public override ResponseType ResponseType
        {
            get
            {
                return ResponseType.JavaScript;
            }
        }

        /// <summary>
        /// 登录状态,5分钟没有成功扫描二维码就会失效
        /// <para>408 - 登录超时</para>
        /// <para>201 - 扫描成功</para>
        /// <para>200 - 确认登录</para>
        /// <para>400 - 二维码已经过期</para>
        /// </summary>
        [Common.Regex("code", @"window.code=(?<code>.+?);")]
        public string Code { get; set; }

        /// <summary>
        /// Code = 201时产生
        /// </summary>
        [Common.Regex(Key = "avatar", RegexPattern = @"window.userAvatar = 'data:img/jpg;base64,(?<avatar>.+?)';")]
        public string UserAvatarBase64String { get; set; }

        /// <summary>
        /// Code = 200时
        /// </summary>
        [Common.Regex(Key = "url", RegexPattern = @"window.redirect_uri=""(?<url>.+?)"";")]
        public string RedirectUri { get; set; }

        public string Host
        {
            get
            {
                if (string.IsNullOrWhiteSpace(RedirectUri))
                    return "wx2.qq.com";
                return new Uri(RedirectUri).Host;
            }
        }

        /// <summary>
        /// Code = 200时
        /// </summary>
        [Common.Regex(Key = "ticket", RegexPattern = @"window.redirect_uri="".+?\?ticket=(?<ticket>.+?)&uuid=(?<uuid>.+?)&lang=(?<lang>.+?)&scan=(?<scan>.+?)"";")]
        public string Ticket { get; set; }

        /// <summary>
        /// Code = 200时
        /// </summary>
        [Common.Regex(Key = "uuid", RegexPattern = @"window.redirect_uri="".+?\?ticket=(?<ticket>.+?)&uuid=(?<uuid>.+?)&lang=(?<lang>.+?)&scan=(?<scan>.+?)"";")]
        public string UUId { get; set; }

        /// <summary>
        /// Code = 200时
        /// </summary>
        [Common.Regex(Key = "lang", RegexPattern = @"window.redirect_uri="".+?\?ticket=(?<ticket>.+?)&uuid=(?<uuid>.+?)&lang=(?<lang>.+?)&scan=(?<scan>.+?)"";")]
        public string Lang { get; set; }

        /// <summary>
        /// Code = 200时
        /// </summary>
        [Common.Regex(Key = "scan", RegexPattern = @"window.redirect_uri="".+?\?ticket=(?<ticket>.+?)&uuid=(?<uuid>.+?)&lang=(?<lang>.+?)&scan=(?<scan>.+?)"";")]
        public string Scan { get; set; }
    }
}
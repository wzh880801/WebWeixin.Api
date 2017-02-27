using System;

namespace Weixin.Api.Common
{
    public static class Extensions
    {
        /// <summary>
        /// 转为JS时间戳
        /// <para>1486083537637</para>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ToJsTimestamp(this DateTime dt)
        {
            var utc = dt.ToUniversalTime();
            var st = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(utc - st).TotalMilliseconds;
        }
    }
}
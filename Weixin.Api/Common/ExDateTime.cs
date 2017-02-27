using System;

namespace Weixin.Api.Common
{
    public struct ExDateTime
    {
        /// <summary>
        /// 现在时间转为JS时间戳
        /// <para>1486083537637</para>
        /// </summary>
        public static long Timestamp
        {
            get
            {
                var now = DateTime.UtcNow;
                var st = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return (long)(now - st).TotalMilliseconds;
            }
        }
    }

    public static class ExDateTimeMethod
    {
        public static string ToUpLoadString(this DateTime t)
        {
            //Thu Nov 10 2016 17:59:27 GMT 0800 (China Standard Time)
            var wek = t.DayOfWeek.ToString().Substring(0, 3);
            var mon = "";
            switch (t.Month)
            {
                case 1:
                    mon = "Jan";
                    break;
                case 2:
                    mon = "Feb";
                    break;
                case 3:
                    mon = "Mar";
                    break;
                case 4:
                    mon = "Apr";
                    break;
                case 5:
                    mon = "May";
                    break;
                case 6:
                    mon = "Jun";
                    break;
                case 7:
                    mon = "Jul";
                    break;
                case 8:
                    mon = "Aug";
                    break;
                case 9:
                    mon = "Sep";
                    break;
                case 10:
                    mon = "Oct";
                    break;
                case 11:
                    mon = "Nov";
                    break;
                case 12:
                    mon = "Dec";
                    break;
            }

            return string.Format("{0} {1} {2:dd} {2:yyyy} {2:HH:mm:ss} GMT+0800 (China Standard Time)", wek, mon, t);
        }
    }
}
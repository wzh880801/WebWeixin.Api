using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Weixin.Api.Helper
{
    public class SerializationHelper
    {
        public static T DeserializeXML<T>(string xml, bool decode = false, string remove = "<br/>")
            where T : class, new()
        {
            if (decode)
                xml = System.Web.HttpUtility.HtmlDecode(xml);

            Regex _regex = new Regex("(?<msg><msg>.+?</msg>)", RegexOptions.IgnoreCase);
            var match = _regex.Match(xml);
            if (match.Success)
                xml = match.Groups["msg"].Value;

            if (!string.IsNullOrWhiteSpace(remove))
                xml = xml.Replace(remove, "");

            xml = xml.Replace("\\\"", "\"").Replace("\\'", "'");

            var bytes = System.Text.Encoding.UTF8.GetBytes(xml);
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                var serializor = new XmlSerializer(typeof(T));
                return serializor.Deserialize(ms) as T;
            }
        }

        public static T DeserializeXML<T>(string xml, Func<string, string> contentHanlder, bool decode = false)
            where T : class, new()
        {
            if (decode)
                xml = System.Web.HttpUtility.HtmlDecode(xml);

            Regex _regex = new Regex("(?<msg><msg>.+?</msg>)", RegexOptions.IgnoreCase);
            var match = _regex.Match(xml);
            if (match.Success)
                xml = match.Groups["msg"].Value;

            xml = contentHanlder(xml);

            var bytes = System.Text.Encoding.UTF8.GetBytes(xml);
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                var serializor = new XmlSerializer(typeof(T));
                return serializor.Deserialize(ms) as T;
            }
        }

        public static T DeserializeXML<T>(string xml, bool decode = false, Regex removeRegex = null)
            where T : class, new()
        {
            if (decode)
                xml = System.Web.HttpUtility.HtmlDecode(xml);

            Regex _regex = new Regex("(?<msg><msg>.+?</msg>)", RegexOptions.IgnoreCase);
            var match = _regex.Match(xml);
            if (match.Success)
                xml = match.Groups["msg"].Value;

            if (removeRegex != null)
                xml = removeRegex.Replace(xml, "");

            xml = xml.Replace("\\\"", "\"").Replace("\\'", "'");

            var bytes = System.Text.Encoding.UTF8.GetBytes(xml);
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                var serializor = new XmlSerializer(typeof(T));
                return serializor.Deserialize(ms) as T;
            }
        }

        public static string SerializeObjectToJson(object o)
        {
            return JsonConvert.SerializeObject(o);
        }
    }
}
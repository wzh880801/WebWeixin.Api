using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Weixin.Api
{
    /// <summary>
    /// Provide the base implementation of IWeixinClient, you could do the all web request using the instance of this client
    /// </summary>
    public class DefaultWeixinClient : IWeixinClient
    {
        public void Dispose()
        {
            _cookie = null;
        }

        /// <summary>
        /// Get cookie
        /// </summary>
        public CookieContainer Cookie
        {
            get
            {
                return _cookie;
            }
        }

        private CookieContainer _cookie = new CookieContainer();
        private string _host = "wx2.qq.com";

        /// <summary>
        /// Set cookie
        /// </summary>
        /// <param name="cookie"></param>
        public void SetCookie(CookieContainer cookie)
        {
            if (cookie == null)
                this._cookie = new CookieContainer();
            else
                this._cookie = cookie;
        }

        private T Parse<T>(Entity.WeixinRequest<T> request, byte[] bytes)
            where T : Entity.WeixinResponse
        {
            var responseString = Encoding.UTF8.GetString(bytes);

            if (string.IsNullOrWhiteSpace(responseString))
                return default(T);

            var t = System.Activator.CreateInstance<T>();
            t.ResponseBase64String = Convert.ToBase64String(bytes);

            if (t.ResponseType == Enum.ResponseType.Stream)
            {
                return t;
            }
            else if (t.ResponseType == Enum.ResponseType.JSON)
            {
                var _t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseString);
                _t.ResponseBase64String = t.ResponseBase64String;
                return _t;
            }
            else if (t.ResponseType == Enum.ResponseType.XML)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Position = 0;
                    var serializor = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    var obj = serializor.Deserialize(ms) as T;
                    if (obj != null)
                    {
                        obj.ResponseBase64String = Convert.ToBase64String(bytes);
                    }
                    return obj;
                }
            }
            else if (t.ResponseType == Enum.ResponseType.HTML || t.ResponseType == Enum.ResponseType.JavaScript)
            {
                var ps = t.GetType().GetProperties();
                foreach (var p in ps)
                {
                    var at = p.GetCustomAttribute(typeof(Common.RegexAttribute));
                    if (at != null)
                    {
                        var reg = at as Common.RegexAttribute;
                        var regex = new Regex(reg.RegexPattern, RegexOptions.IgnoreCase);
                        var match = regex.Match(responseString);
                        if (match != null && p.CanWrite)
                        {
                            var propType = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                            if (propType != typeof(System.String))
                                p.SetValue(t, Convert.ChangeType(match.Groups[reg.Key].Value, propType));
                            else
                                p.SetValue(t, match.Groups[reg.Key].Value);
                        }
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// Submit the specified request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public T Execute<T>(Entity.WeixinRequest<T> request)
            where T : Entity.WeixinResponse
        {
            ResetCookie();
            SetHost(request);

            var bytes = Helper.WebHelper.GetRequestBytes(request, ref _cookie);
            var t = this.Parse(request, bytes);
            if (typeof(Entity.ScanQRResponse) == t.GetType())
                this._host = (t as Entity.ScanQRResponse).Host;
            return t;
        }

        public string ExecuteAsString<T>(Entity.WeixinRequest<T> request)
            where T : Entity.WeixinResponse
        {
            ResetCookie();
            SetHost(request);

            return Helper.WebHelper.GetRequestString(request, ref _cookie);
        }

        public async Task<string> ExecuteAsStringAsync<T>(Entity.WeixinRequest<T> request)
            where T : Entity.WeixinResponse
        {
            ResetCookie();
            SetHost(request);

            return await Helper.WebHelper.GetRequestStringAsync(request, _cookie);
        }

        /// <summary>
        /// Async submit the specified request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<T> ExecuteAsync<T>(Entity.WeixinRequest<T> request)
            where T : Entity.WeixinResponse
        {
            ResetCookie();
            SetHost(request);

            var bytes = await Helper.WebHelper.GetRequestBytesAsync(request, _cookie);
            var t = this.Parse(request, bytes);
            if (typeof(Entity.ScanQRResponse) == t.GetType())
                this._host = (t as Entity.ScanQRResponse).Host;
            return t;
        }

        private void ResetCookie()
        {
            CookieContainer _cookies = new CookieContainer();

            CookieCollection c = _cookie.GetCookies(new Uri("https://wx2.qq.com"));
            if (c != null)
            {
                for (int i = 0; i < c.Count; i++)
                {
                    _cookies.Add(new Cookie(c[i].Name, c[i].Value, c[i].Path, c[i].Domain));
                    _cookies.Add(new Cookie(c[i].Name, c[i].Value, c[i].Path, "wx.qq.com"));
                }
            }

            CookieCollection cc = _cookie.GetCookies(new Uri("https://wx.qq.com"));
            if (cc != null)
            {
                for (int i = 0; i < cc.Count; i++)
                {
                    _cookies.Add(new Cookie(cc[i].Name, cc[i].Value, cc[i].Path, cc[i].Domain));
                    _cookies.Add(new Cookie(cc[i].Name, cc[i].Value, cc[i].Path, "wx2.qq.com"));
                }
            }

            _cookie = _cookies;
        }

        /// <summary>
        /// Get webwx_data_ticket from cookie
        /// </summary>
        /// <returns></returns>
        public string GetWxDataTicket()
        {
            var key = "webwx_data_ticket";
            CookieCollection c = _cookie.GetCookies(new Uri("https://wx2.qq.com"));
            if (c != null)
            {
                for (int i = 0; i < c.Count; i++)
                {
                    if (c[i].Name == key)
                        return c[i].Value;
                }
            }

            CookieCollection cc = _cookie.GetCookies(new Uri("https://wx.qq.com"));
            if (cc != null)
            {
                for (int i = 0; i < cc.Count; i++)
                {
                    if (cc[i].Name == key)
                        return cc[i].Value;
                }
            }

            return "";
        }

        /// <summary>
        /// Auto set the api host 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        private void SetHost<T>(Entity.WeixinRequest<T> request)
            where T : Entity.WeixinResponse
        {
            var ps = request.GetType().GetProperties();
            foreach (var p in ps)
            {
                if (p.Name == "Host" && p.CanWrite && p.PropertyType == typeof(System.String))
                {
                    p.SetValue(request, _host);
                    break;
                }
            }
        }
    }
}
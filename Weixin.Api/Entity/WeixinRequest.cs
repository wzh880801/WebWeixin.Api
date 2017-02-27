using System;
using System.Net;
using System.Collections.Generic;

namespace Weixin.Api.Entity
{
    public abstract class WeixinRequest<T>
    {
        /// <summary>
        /// 默认为Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.87 Safari/537.36
        /// <para>如果需要指定浏览器标示请override此属性</para>
        /// </summary>
        public virtual string UserAgent
        {
            get
            {
                return "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.87 Safari/537.36";
            }
        }

        private Enum.HttpMethods _httpMethod = Enum.HttpMethods.GET;
        /// <summary>
        /// 默认为GET，如果需要POST，请override此属性
        /// </summary>
        public virtual Enum.HttpMethods HttpMethod
        {
            get
            {
                return _httpMethod;
            }
            set
            {
                _httpMethod = value;
            }
        }

        private Enum.ContentTypes _contentType = Enum.ContentTypes.WWW_URL_ENCODEED;

        /// <summary>
        /// 默认为www-url-encoded,如果需要改写默认值，请override此属性或者直接赋值
        /// </summary>
        public virtual Enum.ContentTypes ContentType
        {
            get
            {
                return _contentType;
            }
            set
            {
                _contentType = value;
            }
        }

        /// <summary>
        /// 根据ContentType自动设置，如果需要改写，请override
        /// </summary>
        public virtual string ContentTypeHeader
        {
            get
            {
                if (this.ContentType == Enum.ContentTypes.JSON)
                    return "application/json;charset=UTF-8";
                else if (this.ContentType == Enum.ContentTypes.WWW_URL_ENCODEED)
                    return "application/x-www-form-urlencoded; charset=UTF-8";
                else
                    return "";
            }
        }

        public abstract string Accept { get; }
        public abstract string ApiUrl { get; }
        public abstract string QueryString { get; }
        //public virtual CookieContainer Cookie { get; set; }

        public virtual byte[] QueryBytes { get; }

        private string _acceptEncoding = "gzip, deflate";
        public virtual string AcceptEncoding
        {
            get
            {
                return _acceptEncoding;
            }
            set
            {
                _acceptEncoding = value;
                SetHeader("Accept-Encoding", value);
            }
        }

        private string _acceptLanguage = "en-US,en;q=0.8,zh-CN;q=0.6,zh;q=0.4";
        public virtual string AcceptLanguage
        {
            get
            {
                return _acceptLanguage;
            }
            set
            {
                _acceptLanguage = value;

                SetHeader("Accept-Language", value);
            }
        }

        private bool _ajax = false;
        public virtual bool Ajax
        {
            get
            {
                return _ajax;
            }
            set
            {
                _ajax = value;
                if (value)
                    SetHeader("X-Requested-With", "XMLHttpRequest");
                else
                    RemoveHeader("X-Requested-With");
            }
        }

        private bool _keepAlive = false;
        public virtual bool KeepAlive
        {
            get
            {
                return _keepAlive;
            }
            set
            {
                _keepAlive = value;
            }
        }

        private bool _allowAutoRedirect = false;
        public virtual bool AllowAutoRedirect
        {
            get { return _allowAutoRedirect; }
            set { _allowAutoRedirect = value; }
        }

        /// <summary>
        /// 如果不需要Referer，请设置为null
        /// </summary>
        public virtual string Referer { get; set; }

        private string _origin = null;
        /// <summary>
        /// 如果不需要Origin属性，请设置为null
        /// </summary>
        public virtual string Origin
        {
            get
            {
                return _origin;
            }
            set
            {
                _origin = value;
                if (!string.IsNullOrWhiteSpace(value))
                    SetHeader("Origin", value);
                else
                    RemoveHeader("Origin");
            }
        }

        private void SetHeader(string key, string value)
        {
            if (!_headers.ContainsKey(key))
                _headers.Add(key, value);
            else
                _headers[key] = value;
        }

        private void RemoveHeader(string key)
        {
            if (_headers.ContainsKey(key))
                _headers.Remove(key);
        }

        private Dictionary<string, string> _headers = new Dictionary<string, string>();

        public virtual Dictionary<string, string> Headers
        {
            get
            {
                return _headers;
            }
        }

        public WeixinRequest()
        {
            this.AcceptEncoding = "gzip, deflate";
            this.AcceptLanguage= "en-US,en;q=0.8,zh-CN;q=0.6,zh;q=0.4";
        }
    }
}
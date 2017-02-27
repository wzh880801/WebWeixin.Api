using System;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weixin.Api.Helper
{
    public class WebHelper
    {
        #region sync
        private static string GetResponse(HttpWebRequest request)
        {
            var responseString = "";

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if ((int)response.StatusCode >= 400)
                {
                    return "{\"httpStatusCode\":" + response.StatusCode + "}";
                }
                var encoding = response.Headers["Content-Encoding"];

                using (Stream s = response.GetResponseStream())
                {
                    if (!string.IsNullOrEmpty(encoding))
                    {
                        if (encoding.ToLower().Contains("gzip"))
                        {
                            GZipStream st = new GZipStream(s, CompressionMode.Decompress);
                            using (StreamReader r = new StreamReader(st, Encoding.UTF8))
                            {
                                responseString = r.ReadToEnd();
                            }
                        }
                        else if (encoding.ToLower().Contains("deflate"))
                        {
                            DeflateStream st = new DeflateStream(s, CompressionMode.Decompress);
                            using (StreamReader r = new StreamReader(st, Encoding.UTF8))
                            {
                                responseString = r.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader r = new StreamReader(s, Encoding.UTF8))
                        {
                            responseString = r.ReadToEnd();
                        }
                    }
                }
            }

            return responseString;
        }

        private static byte[] GetResponseBytes(HttpWebRequest request)
        {
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if ((int)response.StatusCode >= 400)
                {
                    //var s = "{\"httpStatusCode\":" + response.StatusCode + "}";
                    //var bytes = Encoding.UTF8.GetBytes(s);
                    //ms.Write(bytes, 0, bytes.Length);
                    //ms.Position = 0;
                    return null;
                }
                //if((int)response.StatusCode == 301)
                //{
                //    return response.GetResponseStream
                //}
                var encoding = response.Headers["Content-Encoding"];
                using (Stream s = response.GetResponseStream())
                {
                    if (!string.IsNullOrEmpty(encoding))
                    {
                        if (encoding.ToLower().Contains("gzip"))
                        {
                            GZipStream st = new GZipStream(s, CompressionMode.Decompress);
                            return ReadStreamBytes(st);
                        }
                        else if (encoding.ToLower().Contains("deflate"))
                        {
                            DeflateStream st = new DeflateStream(s, CompressionMode.Decompress);
                            return ReadStreamBytes(st);
                        }
                    }
                    else
                    {
                        return ReadStreamBytes(s);
                    }
                }
            }

            return null;
        }

        private static byte[] ReadStreamBytes(Stream sourceStream)
        {
            var bytes = new List<byte>();
            int bufferSize = 1024;
            using (BinaryReader br = new BinaryReader(sourceStream, Encoding.UTF8))
            {
                while (true)
                {
                    byte[] buffer = br.ReadBytes(bufferSize);
                    bytes.AddRange(buffer);
                    if (buffer.Length != bufferSize)
                        break;
                }
            }
            return bytes.ToArray();
        }

        private static void WriteRequestParas(HttpWebRequest request, string query)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(query);
            request.ContentLength = buffer.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(buffer, 0, buffer.Length);
            }
        }

        private static void WriteRequestParas(HttpWebRequest request, byte[] query)
        {
            request.ContentLength = query.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(query, 0, query.Length);
            }
        }

        public static string GetRequestString<T>(Entity.WeixinRequest<T> _request, ref CookieContainer cookie)
        {
            var request = CreateRequest(_request, ref cookie);

            if (_request.HttpMethod == Enum.HttpMethods.POST)
            {
                if (_request.QueryBytes == null || _request.QueryBytes.Length == 0)
                {
                    if (!string.IsNullOrWhiteSpace(_request.QueryString))
                        WriteRequestParas(request, _request.QueryString);
                    else
                        request.ContentLength = 0;
                }
                else
                    WriteRequestParas(request, _request.QueryBytes);
            }

            return GetResponse(request);
        }

        public static byte[] GetRequestBytes<T>(Entity.WeixinRequest<T> _request, ref CookieContainer cookie)
        {
            var request = CreateRequest(_request, ref cookie);

            if (_request.HttpMethod == Enum.HttpMethods.POST)
            {
                if (_request.QueryBytes == null || _request.QueryBytes.Length == 0)
                {
                    if (!string.IsNullOrWhiteSpace(_request.QueryString))
                        WriteRequestParas(request, _request.QueryString);
                    else
                        request.ContentLength = 0;
                }
                else
                {
                    WriteRequestParas(request, _request.QueryBytes);
                }
            }

            return GetResponseBytes(request);
        }

        private static HttpWebRequest CreateRequest<T>(Entity.WeixinRequest<T> _request, ref CookieContainer cookie)
        {
            var request = WebRequest.Create(_request.ApiUrl) as HttpWebRequest;
            request.Accept = _request.Accept;
            request.UserAgent = _request.UserAgent;
            request.CookieContainer = cookie;
            request.AllowAutoRedirect = _request.AllowAutoRedirect;

            if (_request.HttpMethod == Enum.HttpMethods.GET)
                request.Method = "GET";
            else if (_request.HttpMethod == Enum.HttpMethods.POST)
                request.Method = "POST";

            if (!string.IsNullOrWhiteSpace(_request.ContentTypeHeader))
                request.ContentType = _request.ContentTypeHeader;

            if (_request.KeepAlive)
                request.KeepAlive = true;

            if (!string.IsNullOrWhiteSpace(_request.Referer))
                request.Referer = _request.Referer;

            if (_request.Headers != null)
            {
                foreach (var key in _request.Headers.Keys)
                    request.Headers.Add(key, _request.Headers[key]);
            }

            return request;
        }
        #endregion

        #region async
        private static async Task<string> GetResponseAsync(HttpWebRequest request)
        {
            var responseString = "";

            using (var response = await request.GetResponseAsync() as HttpWebResponse)
            {
                if ((int)response.StatusCode >= 400)
                {
                    return "{\"httpStatusCode\":" + response.StatusCode + "}";
                }
                var encoding = response.Headers["Content-Encoding"];
                using (Stream s = response.GetResponseStream())
                {
                    if (!string.IsNullOrEmpty(encoding))
                    {
                        if (encoding.ToLower().Contains("gzip"))
                        {
                            GZipStream st = new GZipStream(s, CompressionMode.Decompress);
                            using (StreamReader r = new StreamReader(st, Encoding.UTF8))
                            {
                                responseString = await r.ReadToEndAsync();
                            }
                        }
                        else if (encoding.ToLower().Contains("deflate"))
                        {
                            DeflateStream st = new DeflateStream(s, CompressionMode.Decompress);
                            using (StreamReader r = new StreamReader(st, Encoding.UTF8))
                            {
                                responseString = await r.ReadToEndAsync();
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader r = new StreamReader(s, Encoding.UTF8))
                        {
                            responseString = await r.ReadToEndAsync();
                        }
                    }
                }
            }

            return responseString;
        }

        private static async Task<byte[]> GetResponseBytesAsync(HttpWebRequest request)
        {
            using (var response = await request.GetResponseAsync() as HttpWebResponse)
            {
                if ((int)response.StatusCode >= 400)
                {
                    //var s = "{\"httpStatusCode\":" + response.StatusCode + "}";
                    //var bytes = Encoding.UTF8.GetBytes(s);
                    //ms.Write(bytes, 0, bytes.Length);
                    //ms.Position = 0;
                    return null;
                }
                var encoding = response.Headers["Content-Encoding"];
                using (Stream s = response.GetResponseStream())
                {
                    if (!string.IsNullOrEmpty(encoding))
                    {
                        if (encoding.ToLower().Contains("gzip"))
                        {
                            GZipStream st = new GZipStream(s, CompressionMode.Decompress);
                            return ReadStreamBytes(st);
                        }
                        else if (encoding.ToLower().Contains("deflate"))
                        {
                            DeflateStream st = new DeflateStream(s, CompressionMode.Decompress);
                            return ReadStreamBytes(st);
                        }
                    }
                    else
                    {
                        return ReadStreamBytes(s);
                    }
                }
            }

            return null;
        }

        public static async Task<string> GetRequestStringAsync<T>(Entity.WeixinRequest<T> _request, CookieContainer cookie)
        {
            var request = CreateRequest(_request, ref cookie);

            if (_request.HttpMethod == Enum.HttpMethods.POST)
            {
                if (_request.QueryBytes == null || _request.QueryBytes.Length == 0)
                {
                    if (!string.IsNullOrWhiteSpace(_request.QueryString))
                        WriteRequestParas(request, _request.QueryString);
                    else
                        request.ContentLength = 0;
                }
                else
                {
                    WriteRequestParas(request, _request.QueryBytes);
                }
            }

            return await GetResponseAsync(request);
        }

        public static async Task<byte[]> GetRequestBytesAsync<T>(Entity.WeixinRequest<T> _request, CookieContainer cookie)
        {
            var request = CreateRequest(_request, ref cookie);

            if (_request.HttpMethod == Enum.HttpMethods.POST)
            {
                if (_request.QueryBytes == null || _request.QueryBytes.Length == 0)
                {
                    if (!string.IsNullOrWhiteSpace(_request.QueryString))
                        WriteRequestParas(request, _request.QueryString);
                    else
                        request.ContentLength = 0;
                }
                else
                {
                    WriteRequestParas(request, _request.QueryBytes);
                }
            }

            return await GetResponseBytesAsync(request);
        }
        #endregion
    }
}
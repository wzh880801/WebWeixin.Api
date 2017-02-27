using System;
using System.Net;
using System.Threading.Tasks;

namespace Weixin.Api
{
    public interface IWeixinClient : IDisposable
    {
        CookieContainer Cookie { get; }

        T Execute<T>(Entity.WeixinRequest<T> request)
            where T : Entity.WeixinResponse;

        string ExecuteAsString<T>(Entity.WeixinRequest<T> request)
            where T : Entity.WeixinResponse;

        Task<T> ExecuteAsync<T>(Entity.WeixinRequest<T> request)
            where T : Entity.WeixinResponse;

        Task<string> ExecuteAsStringAsync<T>(Entity.WeixinRequest<T> request)
            where T : Entity.WeixinResponse;

        string GetWxDataTicket();

        void SetCookie(CookieContainer cookie);
    }
}
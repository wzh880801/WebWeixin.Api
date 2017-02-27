using System;
using System.Net;

namespace Weixin.Api.Entity
{
    public class PreLoginRequest : WeixinRequest<PreLoginResponse>
    {
        public override string ApiUrl
        {
            get
            {
                return "https://wx.qq.com/";
            }
        }

        public override string QueryString
        {
            get
            {
                return null;
            }
        }

        public override string Accept
        {
            get
            {
                return "text/html, application/xhtml+xml, image/jxr, */*";
            }
        }

        public PreLoginRequest()
            : base()
        {

        }
    }
}
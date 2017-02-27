using System;

namespace Weixin.Api.Entity
{
    public class GetQRCodeRequest : WeixinRequest<GetQRCodeResponse>
    {
        public override string ApiUrl
        {
            get
            {
                return string.Format(
                    "https://login.wx2.qq.com/jslogin?appid={0}&redirect_uri={1}&fun={2}&lang={3}&_={4}",
                    "wx782c26e4c19acffb",
                    "https%3A%2F%2Fwx.qq.com%2Fcgi-bin%2Fmmwebwx-bin%2Fwebwxnewloginpage",
                    "new",
                    "zh_CN",
                    Common.ExDateTime.Timestamp);
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
                return "application/javascript, */*;q=0.8";
            }
        }

        public GetQRCodeRequest()
            : base()
        {
            this.Referer= "https://wx.qq.com/";
        }
    }
}
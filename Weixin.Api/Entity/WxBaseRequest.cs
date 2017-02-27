using System;

namespace Weixin.Api.Entity
{
    public class WxBaseRequest
    {
        [Newtonsoft.Json.JsonProperty("BaseRequest")]
        public WxRequest BaseRequest { get; set; }

        public WxBaseRequest() { }

        public WxBaseRequest(WxRequest reuest) 
            : this()
        {
            this.BaseRequest = reuest;
        }
    }

    public class WxRequest
    {
        //return "e" + ("" + Math.random().toFixed(15)).substring(2, 17) }
        private string _deviceId = "e941177721273756";

        public WxRequest()
        {
            var str = Math.Round(new Random(Guid.NewGuid().GetHashCode()).NextDouble(), 15).ToString();
            _deviceId = "e" + str.Substring(2, str.Length - 2);
        }

        public WxRequest(LoginWxResponse _loginResponse)
            : this()
        {
            this.Sid = _loginResponse.WxSid;
            this.Skey = _loginResponse.SKey;
            this.Uin = _loginResponse.WxUin;
        }

        public WxRequest(string sid, string skey, string uin)
            : this()
        {
            this.Sid = sid;
            this.Skey = skey;
            this.Uin = uin;
        }

        [Newtonsoft.Json.JsonProperty("Uin")]
        public string Uin { get; set; }

        [Newtonsoft.Json.JsonProperty("Sid")]
        public string Sid { get; set; }

        [Newtonsoft.Json.JsonProperty("Skey")]
        public string Skey { get; set; }

        [Newtonsoft.Json.JsonProperty("DeviceID")]
        public string DeviceID { get { return _deviceId; } }
    }
}

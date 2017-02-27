using System;

namespace Weixin.Api.Entity
{
    /// <summary>
    /// 通过好友验证
    /// 当<see cref="WxSyncResponse.AddMsgList"/>中的<see cref="WxAddMsg.MsgType"/>==37的时候可以使用此请求通过好友申请
    /// </summary>
    public class VerifyUserRequest : WeixinRequest<VerifyUserResponse>
    {
        public string Host { get; set; }

        public string PassTicket { get; set; }

        public override string ApiUrl
        {
            get
            {
                return string.Format("https://{0}/cgi-bin/mmwebwx-bin/webwxverifyuser?r={1}&pass_ticket={2}",
                    this.Host,
                    Common.ExDateTime.Timestamp,
                    this.PassTicket);
            }
        }

        public override string QueryString
        {
            get
            {
                var request = new _VerifyUserRequest(this.Request, this.RecommendInfo);
                return Newtonsoft.Json.JsonConvert.SerializeObject(request);
            }
        }

        public override string Accept
        {
            get
            {
                return "application/json, text/plain, */*";
            }
        }

        public WxRequest Request { get; set; }

        public WxRecommendInfo RecommendInfo { get; set; }

        public VerifyUserRequest()
            : base()
        {
            this.Host = "wx2.qq.com";
            this.Origin = "https://wx2.qq.com";
            this.HttpMethod = Enum.HttpMethods.POST;
        }

        public VerifyUserRequest(WxRequest request, WxRecommendInfo info, string passTicket)
            : this()
        {
            this.Request = request;
            this.RecommendInfo = info;
            this.PassTicket = passTicket;
        }

        public class _VerifyUserRequest
        {
            public WxRequest BaseRequest { get; set; }
            /// <summary>
            /// 3
            /// </summary>
            public int Opcode { get; set; }

            /// <summary>
            /// 1 <seealso cref="VerifyUserList.Length"/>
            /// </summary>
            public int VerifyUserListSize { get { return this.VerifyUserList.Length; } }

            public Verifyuserlist[] VerifyUserList { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string VerifyContent { get; set; }
            /// <summary>
            /// 1
            /// </summary>
            public int SceneListCount { get { return this.SceneList.Length; } }
            /// <summary>
            /// [33]
            /// </summary>
            public int[] SceneList { get; set; }
            public string skey { get; set; }

            public _VerifyUserRequest()
            {
                this.Opcode = 3;
                this.VerifyContent = "";
            }

            public _VerifyUserRequest(WxRequest request, WxRecommendInfo recommendInfo)
                : this()
            {
                this.BaseRequest = request;
                this.skey = request.Skey;
                this.VerifyUserList = new Verifyuserlist[] {new Verifyuserlist{
                        Value = recommendInfo.UserName,
                        VerifyUserTicket = recommendInfo.Ticket } };
                this.SceneList = new int[] { 33 };
            }
        }

        public class Verifyuserlist
        {
            /// <summary>
            /// <seealso cref="WxRecommendInfo.UserName"/>
            /// </summary>
            public string Value { get; set; }

            /// <summary>
            /// <seealso cref="WxRecommendInfo.Ticket"/>
            /// </summary>
            public string VerifyUserTicket { get; set; }
        }
    }
}
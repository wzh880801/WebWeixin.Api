using System;

namespace Weixin.Api.Entity
{
    public abstract class WeixinResponse
    {
        [Newtonsoft.Json.JsonProperty("httpStatusCode")]
        public virtual int HttpStatusCode { get; }

        private Enum.ResponseType _reponseType = Enum.ResponseType.HTML;
        public virtual Enum.ResponseType ResponseType
        {
            get
            {
                return _reponseType;
            }
        }

        public virtual string ResponseBase64String { get; set; }
    }
}
using System;

namespace Weixin.Api.Common
{
    [System.AttributeUsage(AttributeTargets.Property)]
    public class RegexAttribute : Attribute
    {
        public RegexAttribute(string key, string pattern)
        {
            this.Key = key;
            this.RegexPattern = pattern;
        }

        public RegexAttribute()
        {

        }

        public string Key { get; set; }
        public string RegexPattern { get; set; }
    }
}
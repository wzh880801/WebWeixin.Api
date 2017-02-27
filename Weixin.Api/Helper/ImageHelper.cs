using System;
using System.IO;
using System.Drawing;

namespace Weixin.Api.Helper
{
    public static class ImageHelper
    {
        public static Image FromBase64String(string base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                var bytes = Convert.FromBase64String(base64String);
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;

                return Image.FromStream(ms);
            }
        }
    }
}
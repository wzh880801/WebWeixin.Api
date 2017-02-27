using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Weixin.Api.Helper
{
    public static class FileHelper
    {
        public static string GetFileMd5(FileInfo _file)
        {
            if (!_file.Exists)
                throw new FileNotFoundException("未找到文件", nameof(_file.FullName));

            using (FileStream file = new FileStream(_file.FullName, System.IO.FileMode.Open))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static string GetFileType(FileInfo _file)
        {
            switch (_file.Extension.ToLower())
            {
                case ".png":
                    return "image/png";
                case ".jpg":
                    return "image/jpeg";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                case ".csv":
                    return "application/vnd.ms-excel";
                case ".txt":
                    return "text/plain";
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            return "text/plain";
        }

        public static string GetMediaType(FileInfo _file)
        {
            switch (_file.Extension.ToLower())
            {
                case ".png":
                    return "pic";
                case ".jpg":
                    return "pic";
                case ".gif":
                    return "pic";
                case ".bmp":
                    return "pic";
                case ".csv":
                    return "doc";
                case ".txt":
                    return "doc";
                case ".xlsx":
                    return "doc";
            }

            return "doc";
        }

        //public static string GetFileContent(FileInfo _file)
        //{

        //}
    }
}
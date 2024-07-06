using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace QuickFire.Utils
{
    public static class EncryptUtils
    {
        //MD5加密

        public static string EncryptStringToMd5(string str)
        {
            // 创建MD5加密对象
            using (MD5 md5Hash = MD5.Create())
            {
                // 将输入字符串转换为字节序列
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str));
                // 将计算出的哈希值转换为字符串表示形式
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                // 返回32位的十六进制字符串
                return sBuilder.ToString();
            }
        }
    }
}

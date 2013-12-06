﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Library.Common
{
    public class EncryptDecrypt
    {
        #region 加/解密方法
        private static string CryKey = "Xky_Lq_Py_Hu_Lp_Jhj_Zxt";//密钥
        /// <summary>
        /// 加密字符串
        /// </summary>
        public static string Encrypt(string PlainText)
        {
            System.Security.Cryptography.DESCryptoServiceProvider key = new System.Security.Cryptography.DESCryptoServiceProvider();
            byte[] bk = System.Text.Encoding.Unicode.GetBytes(CryKey);
            byte[] bs = new byte[8];
            for (int i = 0; i < bs.Length; i++)
            {
                bs[i] = bk[i];
            }
            key.Key = bs;
            key.IV = new byte[] { 8, 7, 6, 5, 4, 3, 2, 1 };

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            System.Security.Cryptography.CryptoStream encStream = new System.Security.Cryptography.CryptoStream(ms, key.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            System.IO.StreamWriter sw = new System.IO.StreamWriter(encStream);
            sw.WriteLine(PlainText);
            sw.Close();
            encStream.Close();

            byte[] buffer = ms.ToArray();
            ms.Close();

            string s = "";
            for (int i = 0; i < buffer.Length; i++)
            {
                s += buffer[i].ToString("X2");
            }
            return s;
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        public static string Decrypt(string CypherText)
        {
            System.Security.Cryptography.DESCryptoServiceProvider key = new System.Security.Cryptography.DESCryptoServiceProvider();
            byte[] bk = System.Text.Encoding.Unicode.GetBytes(CryKey);
            byte[] bs = new byte[8];
            for (int i = 0; i < bs.Length; i++)
            {
                bs[i] = bk[i];
            }
            key.Key = bs;
            key.IV = new byte[] { 8, 7, 6, 5, 4, 3, 2, 1 };

            byte[] bc = new byte[CypherText.Length / 2];
            for (int i = 0; i < bc.Length; i++)
            {
                try
                {
                    bc[i] = Convert.ToByte(CypherText.Substring(2 * i, 2), 16);
                }
                catch { }
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bc);
            System.Security.Cryptography.CryptoStream encStream = new System.Security.Cryptography.CryptoStream(ms, key.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Read);
            System.IO.StreamReader sr = new System.IO.StreamReader(encStream);

            string val = sr.ReadLine();

            sr.Close();
            encStream.Close();
            ms.Close();

            return val;
        }
        #endregion

        #region 不可逆加密方法
        /// <summary>
        /// MD5加密不可逆
        /// </summary>
        /// <param name="str">要加密的字符窜</param>
        /// <returns>返回加密后的字符窜</returns>
        public static string EncryptByMD5Hash(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
            //Encoding.ASCII.GetBytes(inputString)用于以ASCII方式将一个字符串转换成一个字节数组,
            //原因是ComputeHash方法只接收Byte[]参数,后面的内容就是将加密后的Byte[]连成一个字符串,
            //AppendFormat中的格式字符串{0:x2}是指将数组中每一个字符格式化为十六进制,精度为2
        }
        #endregion
    }
}

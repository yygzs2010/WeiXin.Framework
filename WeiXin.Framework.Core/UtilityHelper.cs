using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Xml.Linq;  
    

namespace WeiXin.Framework.Core
{
    /// <summary>
    /// 微信通用处理工具类
    /// </summary>
    public class UtilityHelper
    {
        #region 字段/属性

        /// <summary>
        /// 微信自定义密钥常量
        /// </summary>
        private static readonly string Token;

        #endregion

        #region 构造函数

        static UtilityHelper()
        {
            Token = ConfigurationManager.ConnectionStrings["WeiXinToken"].ConnectionString;
        }

        #endregion

        #region 方法

        #region 检查加密签名是否一致 - public static bool CheckSignature(string signature, string timestamp, string nonce)

        /// <summary>
        /// 检查加密签名是否一致
        /// </summary>
        /// <param name="signature">微信加密签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <returns></returns>
        public static bool CheckSignature(string signature, string timestamp, string nonce)
        {
            List<string> stringList = new List<string> {Token, timestamp, nonce};
            // 字典排序
            stringList.Sort();
            return Sha1Encrypt(string.Join("", stringList)) == signature;
        }

        #endregion

        #region 对字符串SHA1加密 - public static string Sha1Encrypt(string targetString)

        /// <summary>
        /// 对字符串SHA1加密
        /// </summary>
        /// <param name="targetString">源字符串</param>
        /// <returns>加密后的十六进制字符串</returns>
        private static string Sha1Encrypt(string targetString)
        {
            byte[] byteArray = Encoding.Default.GetBytes(targetString);
            HashAlgorithm hashAlgorithm = new SHA1CryptoServiceProvider();
            byteArray = hashAlgorithm.ComputeHash(byteArray);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte item in byteArray)
            {
                stringBuilder.AppendFormat("{0:x2}", item);
            }
            return stringBuilder.ToString();
        }

        #endregion

        #region 根据加密类型对字符串SHA1加密 - public static string Sha1Encrypt(string targetString, string encryptType)

        /// <summary>
        /// 根据加密类型对字符串SHA1加密
        /// </summary>
        /// <param name="targetString">源字符串</param>
        /// <param name="encryptType">加密类型：MD5/SHA1</param>
        /// <returns>加密后的字符串</returns>
        private static string Sha1Encrypt(string targetString, string encryptType)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(targetString, encryptType);
        }

        #endregion

        #endregion

        /// <summary>
        /// ParseJson
        /// </summary>
        /// <param name="json"></param>
        /// <param name="rootName"></param>
        /// <returns></returns>
        public static XDocument ParseJson(string json, string rootName)
        {
            return JsonConvert.DeserializeXNode(json, rootName);
        }


    }
}

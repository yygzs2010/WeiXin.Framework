using System;
using System.Xml.Linq;
using System.Configuration;

namespace WeiXin.Framework.Core
{
    public class UtilityAccessToken
    {
        public static string AppID = ConfigurationManager.ConnectionStrings["appid"].ConnectionString;

        public static string AppSecret = ConfigurationManager.ConnectionStrings["appsecret"].ConnectionString;

        private static DateTime GetAccessToken_Time;
        /// <summary>
        /// 过期时间为7200秒
        /// </summary>
        private static int Expires_Period = 7200;
        /// <summary>
        /// 
        /// </summary>
        private static string mAccessToken;
        /// <summary>
        /// 
        /// </summary>
        public static string AccessToken
        {
            get
            {
                //如果为空，或者过期，需要重新获取
                if (string.IsNullOrEmpty(mAccessToken) || HasExpired())
                {
                    //获取
                    mAccessToken = GetAccessToken(AppID, AppSecret);
                }

                return mAccessToken;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        private static string GetAccessToken(string appId, string appSecret)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, appSecret);
            string result = HttpUtility.GetData(url);

            XDocument doc = UtilityHelper.ParseJson(result, "root");
            XElement root = doc.Root;
            if (root != null)
            {
                XElement access_token = root.Element("access_token");
                if (access_token != null)
                {
                    GetAccessToken_Time = DateTime.Now;

                    if (root.Element("expires_in") != null)
                    {
                        Expires_Period = int.Parse(root.Element("expires_in").Value);
                    }

                    return access_token.Value;
                }
                else
                {
                    GetAccessToken_Time = DateTime.MinValue;
                }
            }

            return null;
        }

        /// <summary>
        /// GetUserInfo
        /// </summary>
        /// <param name="openID"></param>
        /// <returns></returns>
        public static string GetUserInfo(string openID)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}", AccessToken, openID);
            string result = HttpUtility.GetData(url);
            string nick_name = string.Empty;
            XDocument doc = UtilityHelper.ParseJson(result, "root");
            XElement root = doc.Root;

            if (root != null)
            {
                XElement nickname = root.Element("nickname");
                nick_name = nickname.Value;
            }

            return nick_name;
        }

        /// <summary>
        /// 判断Access_token是否过期
        /// </summary>
        /// <returns>bool</returns>
        private static bool HasExpired()
        {
            if (GetAccessToken_Time != null)
            {
                //过期时间，允许有一定的误差，一分钟。获取时间消耗
                if (DateTime.Now > GetAccessToken_Time.AddSeconds(Expires_Period).AddSeconds(-60))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

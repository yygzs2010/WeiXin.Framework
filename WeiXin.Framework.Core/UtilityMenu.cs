using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXin.Framework.Core
{
    public class UtilityMenu
    {
        /// <summary>
        /// 菜单文件路径
        /// </summary>
        private static readonly string Menu_Data_Path = System.AppDomain.CurrentDomain.BaseDirectory + "/Data/menu.txt";
        /// <summary>
        /// 获取菜单
        /// </summary>
        public static string GetMenu()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", UtilityAccessToken.AccessToken);

            return HttpUtility.GetData(url);
        }
        /// <summary>
        /// 创建菜单
        /// </summary>
        public static void CreateMenu(string menu)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", UtilityAccessToken.AccessToken);
            //string menu = FileUtility.Read(Menu_Data_Path);
            HttpUtility.SendHttpRequest(url, menu);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        public static void DeleteMenu()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", UtilityAccessToken.AccessToken);
            HttpUtility.GetData(url);
        }
        /// <summary>
        /// 加载菜单
        /// </summary>
        /// <returns></returns>
        public static string LoadMenu()
        {
            //return FileUtility.Read(Menu_Data_Path);
            return string.Empty;
        }

    }
}

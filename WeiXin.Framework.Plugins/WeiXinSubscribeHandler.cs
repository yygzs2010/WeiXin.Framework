using System;
using System.Xml.Linq;

using WeiXin.Framework.Core;

namespace WeiXin.Framework.Plugins
{
    /// <summary>
    /// 微信用户关注事件控制器
    /// </summary>
    public class WeiXinSubscribeHandler : WeiXinHandlerType
    {
        //Log4Net
        public static readonly log4net.ILog s_Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region 字段/属性
        /// <summary>
        /// 消息类型Event
        /// </summary>
        public override WeiXinMsgType WeiXinMsgType
        {
            get { return WeiXinMsgType.Event; }
        }

        /// <summary>
        /// 用户关注事件
        /// </summary>
        public override WeiXinEventType WeiXinEventType
        {
            get { return WeiXinEventType.SubScribe; }
        }
        #endregion

        public override void ProcessWeiXin(WeiXinContext context)
        {
            string userInfo = string.Empty;
            string nick_name = UtilityAccessToken.GetUserInfo(context.Request.FromUserName);

            UtilityMenu.DeleteMenu();
            string menu = "{" +
    "\"button\": [" +
        "{" +
            "\"name\": \"关于我们\"," +
            "\"sub_button\": [" +
                "{" +
                    "\"type\": \"view\"," +
                    "\"name\": \"关于我们\"," +
                    "\"url\": \"http://www.soyisoft.cn/WebUI/pages/a5001.aspx\"" +
                "}," +
                "{" +
                    "\"type\": \"view\"," +
                    "\"name\": \"信息中心\"," +
                    "\"url\": \"http://www.soyisoft.cn/WebUI/pages/a1001.aspx\"" +
                "}" +
            "]" +
        "}," +
        "{" +
            "\"name\": \"产品方案\"," +
            "\"sub_button\": [" +
                "{" +
                    "\"type\": \"view\"," +
                    "\"name\": \"产品列表\"," +
                    "\"url\": \"http://www.soyisoft.cn/WeiXin/Ui/Products/ShowProduct.aspx?shop_id=1\"" +
                "}," +
                "{" +
                    "\"type\": \"view\"," +
                    "\"name\": \"店铺列表\"," +
                    "\"url\": \"http://www.soyisoft.cn/WeiXin/Ui/Shop/ShopPreview.htm?shop_id=1\"" +
                "}," +
                "{" +
                    "\"type\": \"view\"," +
                    "\"name\": \"活动列表\"," +
                    "\"url\": \"http://www.soyisoft.cn/WeiXin/Ui/Activity/ActViewList.aspx?shop_id=1\"" +
                "}" +
            "]" +
        "}," +
        "{" +
            "\"name\": \"沃尔沃\"," +
            "\"sub_button\": [" +
                "{" +
                    "\"type\": \"view\"," +
                    "\"name\": \"报修登记\"," +
                    "\"url\": \"http://t.vdis.cn/BI/Ui/Wx/RepairEdit.aspx\"" +
                "}," +
                "{" +
                    "\"type\": \"view\"," +
                    "\"name\": \"报修查询\"," +
                    "\"url\": \"http://t.vdis.cn/BI/Ui/Wx/RepairList.aspx\"" +
                "}," +
                "{" +
                    "\"type\": \"view\"," +
                    "\"name\": \"经验分享\"," +
                    "\"url\": \"http://t.vdis.cn/BI/Ui/Wx/ShareList.aspx\"" +
                "}" +
            "]" +
        "}" +
    "]" +
"}";
            menu = menu.Replace(" ", "");
            UtilityMenu.CreateMenu(menu);
            //context.Response.Write(menu);
            //s_Log.Info(this.GetType().ToString() + ":" + menu);
            
            XElement result = new XElement("xml", new XElement("ToUserName", context.Request.FromUserName),
                new XElement("FromUserName", context.Request.ToUserName),
                new XElement("CreateTime", DateTime.Now.GetInt()),
                new XElement("MsgType", WeiXinMsgType.Text.ToString().ToLower()),
                new XElement("Content", nick_name + " 您好：欢迎关注索一软件微信订阅号。"));
            context.Response.Write(result);

            
        }
    }
}

using System;
using System.Xml.Linq;
using WeiXin.Framework.Core;

namespace WeiXin.Framework.Plugins
{
    /// <summary>
    /// 微信用户取消关注事件控制器
    /// </summary>
    public class WeiXinUnSubscribeHandler : WeiXinHandlerType
    {
        #region 字段/属性

        /// <summary>
        /// 消息类型Event
        /// </summary>
        public override WeiXinMsgType WeiXinMsgType
        {
            get { return WeiXinMsgType.Event; }
        }

        /// <summary>
        /// 用户取消关注事件
        /// </summary>
        public override WeiXinEventType WeiXinEventType
        {
            get { return WeiXinEventType.UnSubScribe; }
        }

        #endregion

        public override void ProcessWeiXin(WeiXinContext context)
        {
            XElement result = new XElement("xml", new XElement("ToUserName", context.Request.FromUserName),
                                           new XElement("FromUserName", context.Request.ToUserName),
                                           new XElement("CreateTime", DateTime.Now.GetInt()),
                                           new XElement("MsgType", WeiXinMsgType.Text.ToString().ToLower()),
                                           new XElement("Content", "为什么不爱我?"));
            context.Response.Write(result);
        }
    }
}

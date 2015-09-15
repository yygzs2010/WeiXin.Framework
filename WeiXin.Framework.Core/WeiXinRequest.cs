using System;
using System.Xml.Linq;

namespace WeiXin.Framework.Core
{
    /// <summary>
    /// 微信请求上下文实体类
    /// </summary>
    public class WeiXinRequest
    {
        #region 字段/属性
        /// <summary>
        /// 请求消息XElement类型属性
        /// </summary>
        public XElement RequestXmlElement { get; private set; }
        /// <summary>
        /// 开发者微信账号
        /// </summary>
        public string ToUserName { get; private set; }
        /// <summary>
        /// 发送方微信账号
        /// </summary>
        public string FromUserName { get; private set; }
        /// <summary>
        /// 微信消息创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }
        /// <summary>
        /// 微信消息类型
        /// </summary>
        public WeiXinMsgType WeiXinMsgType { get; private set; }
        /// <summary>
        /// 微信事件消息类型
        /// </summary>
        public WeiXinEventType WeiXinEventType { get; private set; }
        #endregion

        #region 构造函数

        public WeiXinRequest()
        {

        }

        /// <summary>
        /// 构造函数，设置请求上下文的相关属性
        /// </summary>
        /// <param name="requestXml">请求消息xml</param>
        public WeiXinRequest(XElement requestXml)
        {
            try
            {
                RequestXmlElement = requestXml;

                ToUserName = RequestXmlElement.Element("ToUserName").Value;
                FromUserName = RequestXmlElement.Element("FromUserName").Value;
                CreateTime = RequestXmlElement.Element("CreateTime").Value.IntStringToDateTime();
                switch (RequestXmlElement.Element("MsgType").Value)
                {
                    case "text":
                        WeiXinMsgType=WeiXinMsgType.Text;
                        break;
                    case "image":
                        WeiXinMsgType = WeiXinMsgType.Image;
                        break;
                    case "voice":
                        WeiXinMsgType = WeiXinMsgType.Voice;
                        break;
                    case "video":
                        WeiXinMsgType = WeiXinMsgType.Video;
                        break;
                    case "location":
                        WeiXinMsgType = WeiXinMsgType.Location;
                        break;
                    case "link":
                        WeiXinMsgType = WeiXinMsgType.Link;
                        break;
                    case "event":
                        WeiXinMsgType = WeiXinMsgType.Event;
                        switch (RequestXmlElement.Element("Event").Value)
                        {
                            case "subscribe":
                                WeiXinEventType = WeiXinEventType.SubScribe;
                                break;
                            case "SCAN":
                                WeiXinEventType = WeiXinEventType.Scan;
                                break;
                            case "CLICK":
                                WeiXinEventType = WeiXinEventType.Click;
                                break;
                            case "LOCATION":
                                WeiXinEventType = WeiXinEventType.Location;
                                break;
                            case "VIEW":
                                WeiXinEventType = WeiXinEventType.View;
                                break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 获取请求消息T的类型实体对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>类型T的实体对象</returns>
        public T GetRequestModel<T>() where T : class
        {
            if (RequestXmlElement == null)
            {
                return Activator.CreateInstance(typeof(T)) as T;
            }
            return RequestXmlElement.Get<T>();
        }

        #endregion
    }
}

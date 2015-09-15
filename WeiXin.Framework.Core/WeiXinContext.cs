using System.Xml.Linq;

namespace WeiXin.Framework.Core
{
    /// <summary>
    /// 微信上下文
    /// </summary>
    public class WeiXinContext
    {
        #region 字段/属性
        /// <summary>
        /// 微信请求上下文
        /// </summary>
        public WeiXinRequest Request { get; set; }
        /// <summary>
        /// 微信响应上下文
        /// </summary>
        public WeiXinResponse Response { get; set; }
        #endregion

        #region 构造函数

        public WeiXinContext()
        {

        }

        /// <summary>
        /// 构造函数，把请求消息封装到微信请求/响应上下文属性
        /// </summary>
        /// <param name="requestXml">请求消息xml</param>
        public WeiXinContext(XElement requestXml)
        {
            Request=new WeiXinRequest(requestXml);
            Response=new WeiXinResponse();
        }

        #endregion

    }
}

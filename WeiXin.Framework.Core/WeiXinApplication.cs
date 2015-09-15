using System;
using System.Xml.Linq;

namespace WeiXin.Framework.Core
{
    /// <summary>
    /// 处理所有微信请求入口实体类
    /// </summary>
    public class WeiXinApplication
    {
        #region 字段/属性
        /// <summary>
        /// 微信上下文
        /// </summary>
        private WeiXinContext _context;
        #endregion

        #region 构造函数

        public WeiXinApplication()
        {

        }
        /// <summary>
        /// 构造函数，把请求消息封装到微信上下文
        /// </summary>
        /// <param name="requestXml">请求消息Xml</param>
        public WeiXinApplication(XElement requestXml)
        {
            this._context = new WeiXinContext(requestXml);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取响应消息xml格式字符串
        /// </summary>
        /// <returns>响应消息xml格式字符串</returns>
        public string GetResponseXml()
        {
            try
            {
                IWeiXinHandler weiXinHandler = WeiXinHandlerFactory.GetInstance().CreateWeiXinHandler(this._context.Request);
                weiXinHandler.ProcessWeiXin(this._context);
            }
            catch (WeiXinHandlerNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this._context.Response.ResponseXml;
        }

        #endregion
    }
}

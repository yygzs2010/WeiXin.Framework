using System;
using System.Text;
using System.Xml.Linq;

namespace WeiXin.Framework.Core
{
    /// <summary>
    /// 微信响应上下文
    /// </summary>
    public class WeiXinResponse
    {
        #region 字段/属性
        /// <summary>
        /// 响应消息xml
        /// </summary>
        public string ResponseXml { get; private set; }
        #endregion

        #region 构造函数

        public WeiXinResponse()
        {

        }
        #endregion

        #region 方法
        /// <summary>
        /// 输出响应消息
        /// </summary>
        /// <param name="entity">响应消息实体对象</param>
        public void Write(object entity)
        {
            Write(entity.GetXElement());
        }
        /// <summary>
        /// 输出响应消息
        /// </summary>
        /// <param name="byteContent">响应消息二进制格式</param>
        public void Write(byte[] byteContent)
        {
            try
            {
                ResponseXml = Encoding.Default.GetString(byteContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 输出响应消息
        /// </summary>
        /// <param name="element">响应消息XElement对象</param>
        public void Write(XElement element)
        {
            try
            {
                ResponseXml = element.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 输出响应消息
        /// </summary>
        /// <param name="responseXml">响应消息xml格式字符串</param>
        public void Write(string responseXml)
        {
            ResponseXml = responseXml;
        }

        #endregion

    }
}

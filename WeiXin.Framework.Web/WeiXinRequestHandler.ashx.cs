using System;
using System.Web;
using System.Xml;
using System.Xml.Linq;

using WeiXin.Framework.Core;

namespace WeiXin.Framework.Web
{
    /// <summary>
    /// 处理微信请求信息入口控制器
    /// </summary>
    public class WeiXinRequestHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            // 请求方式大写
            string httpMethod = context.Request.HttpMethod.ToUpper();
            // 响应消息结果字符串对象
            object responseXml = string.Empty; 

            try
            {
                switch (httpMethod)
                {
                    // GET方式请求
                    case "GET":
                        string signature = context.Request.QueryString["signature"], // 微信加密签名
                               timestamp = context.Request.QueryString["timestamp"], // 时间戳
                               nonce = context.Request.QueryString["nonce"], // 随机数
                               echostr = context.Request.QueryString["echostr"];// 随机字符串
                        // 微信请求参数非空验证
                        if (!String.IsNullOrEmpty(signature) && !String.IsNullOrEmpty(timestamp) && !String.IsNullOrEmpty(nonce) && !String.IsNullOrEmpty(echostr))
                        {
                            // 检查加密签名是否正确
                            if (UtilityHelper.CheckSignature(signature, timestamp, nonce))
                            {
                                responseXml = echostr;
                            }
                        }
                        break;
                    // POST方式请求
                    case "POST":
                        if (!string.IsNullOrEmpty(context.Request.Form["weixinMsg"]))
                        {
                            // 处理test.html页面测试的请求，并返回信息
                            XElement requestXml = XElement.Parse(context.Request.Form["weixinMsg"]);
                            WeiXinApplication weiXinApplication = new WeiXinApplication(requestXml);
                            responseXml = weiXinApplication.GetResponseXml();
                        }
                        else
                        {
                            // 解析微信请求
                            XElement requestXml = XElement.Load(context.Request.InputStream);
                            // 处理微信消息请求，并返回信息
                            WeiXinApplication weiXinApplication = new WeiXinApplication(requestXml);
                            responseXml = weiXinApplication.GetResponseXml();
                        }
                        break;
                }
            }
            catch (XmlException ex)
            {
                responseXml = string.Format("xml解析异常:{0}", ex.Message);
            }
            catch (WeiXinHandlerNotFoundException ex)
            {
                responseXml = ex.Message;
            }
            catch (Exception ex)
            {
                responseXml = ex.Message;
            }

            context.Response.Clear();
            context.Response.Charset = "UTF-8";
            context.Response.Write(responseXml);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
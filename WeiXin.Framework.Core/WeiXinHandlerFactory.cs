using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using WeiXin.Framework.Core.Entities;

namespace WeiXin.Framework.Core
{
    /// <summary>
    /// 获取微信消息处理控制器工厂实体类（单例模式）
    /// </summary>
    public class WeiXinHandlerFactory
    {
        #region 字段/属性

        /// <summary>
        /// 控制器工厂单例
        /// </summary>
        private static WeiXinHandlerFactory _weiXinHandlerFactory;

        /// <summary>
        /// 插件集合目录
        /// </summary>
        private static readonly string PluginFolder;

        /// <summary>
        /// 实现IWeiXinHandler接口的类插件类型集合
        /// </summary>
        private static readonly List<Type> PluginTypes;

        #endregion

        #region 构造函数

        static WeiXinHandlerFactory()
        {
            // 插件目录
            PluginFolder = AppDomain.CurrentDomain.BaseDirectory + "bin\\" + ConfigurationManager.ConnectionStrings["WeiXinPluginFolder"].ConnectionString;

            PluginTypes = new List<Type>();

            // 获取插件目录下名称匹配为WeiXin.*.dll所有程序集路径
            string[] pathArray = Directory.GetFiles(PluginFolder, "WeiXin.*.dll");

            foreach (string path in pathArray)
            {
                Assembly assembly = Assembly.LoadFile(path);
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    // 类型是否继承IWeiXinHandler接口且不是抽象类
                    if (!typeof(IWeiXinHandler).IsAssignableFrom(type) || type.IsAbstract) continue;
                    PluginTypes.Add(type);
                }
            }
        }

        private WeiXinHandlerFactory()
        {
        }

        #endregion

        #region 方法
        /// <summary>
        /// 获取一个实例
        /// </summary>
        /// <returns>工厂类对象</returns>
        public static WeiXinHandlerFactory GetInstance()
        {
            if (_weiXinHandlerFactory == null)
            {
                _weiXinHandlerFactory = new WeiXinHandlerFactory(); ;
            }
            return _weiXinHandlerFactory;
        }
        /// <summary>
        /// 根据消息类型创建消息控制器
        /// </summary>
        /// <param name="request">微信请求上下文</param>
        /// <returns>实现IWeiXinHandler微信消息处理控制器</returns>
        public IWeiXinHandler CreateWeiXinHandler(WeiXinRequest request)
        {
            IWeiXinHandler weiXinHandler = null;
            try
            {
                foreach (Type pluginType in PluginTypes)
                {
                    WeiXinHandlerType weiXinHandlerType = Activator.CreateInstance(pluginType) as WeiXinHandlerType;

                    if (weiXinHandlerType == null) continue;

                    if (request.WeiXinMsgType != WeiXinMsgType.Event)
                    {
                        if (weiXinHandlerType.WeiXinMsgType == request.WeiXinMsgType)
                        {
                            weiXinHandler = weiXinHandlerType;
                            break;
                        }
                    }
                    else
                    {
                        if (weiXinHandlerType.WeiXinEventType == request.WeiXinEventType)
                        {
                            weiXinHandler = weiXinHandlerType;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if(weiXinHandler==null)
                throw new WeiXinHandlerNotFoundException
                    {
                        ExceptionMessage = new WeiXinTextMessageEntity
                            {
                                ToUserName = request.FromUserName,
                                Content = "控制器未找到异常:此消息类型控制器可能未实现!",
                                MsgType = WeiXinMsgType.Text.ToString().ToLower()
                            }.GetXElement().ToString()
                    };

            return weiXinHandler;
        }

        #endregion
    }
}

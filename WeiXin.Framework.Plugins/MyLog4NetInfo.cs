using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WeiXin.Framework.Plugins
{
    /// <summary>
    ///错误记录日志 
    /// </summary>
    public class MyLog4NetInfo
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("WebLogger");

        public MyLog4NetInfo()
        {

        }

        private static void SetConfig()
        {
            object o = ConfigurationManager.GetSection("log4net");
            log4net.Config.XmlConfigurator.Configure(o as System.Xml.XmlElement);
        }

        public static void LogInfo(string Message)
        {
            if (!log.IsInfoEnabled)
                SetConfig();
            log.Info(Message);
        }

        public static void LogInfo(string Message, Exception ex)
        {
            if (!log.IsInfoEnabled)
                SetConfig();
            log.Info(Message, ex);
        }
        public static void ErrorInfo(string Message)
        {
            if (!log.IsInfoEnabled)
                SetConfig();
            log.Error(Message);
        }

        public static void DebugInfo(string Message)
        {
            if (!log.IsInfoEnabled)
                SetConfig();
            log.Debug(Message);
        }

    }

}

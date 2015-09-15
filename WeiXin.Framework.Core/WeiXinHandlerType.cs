namespace WeiXin.Framework.Core
{
    /// <summary>
    /// 微信消息类型抽象基类
    /// </summary>
    public abstract class WeiXinHandlerType:IWeiXinHandler
    {
        /// <summary>
        /// 微信消息类型
        /// </summary>
        public abstract WeiXinMsgType WeiXinMsgType { get;}
        /// <summary>
        /// 微信事件消息类型
        /// </summary>
        public virtual WeiXinEventType WeiXinEventType {
            get { return WeiXinEventType.None; }
        }
        public abstract void ProcessWeiXin(WeiXinContext context);
    }
}

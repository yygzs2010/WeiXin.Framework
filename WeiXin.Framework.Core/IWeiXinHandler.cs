namespace WeiXin.Framework.Core
{
    /// <summary>
    /// 微信处理控制器请求接口
    /// </summary>
    public interface IWeiXinHandler
    {
        /// <summary>
        /// 处理微信请求
        /// </summary>
        /// <param name="context">微信上下文</param>
        void ProcessWeiXin(WeiXinContext context);
    }
}

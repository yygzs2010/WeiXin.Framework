using WeiXin.Framework.Core;
using WeiXin.Framework.Core.Entities;

namespace WeiXin.Framework.Plugins
{
    /// <summary>
    /// 微信文本消息处理控制器
    /// </summary>
    public class WeiXinTextHandler : WeiXinHandlerType
    {
        #region 字段/属性

        /// <summary>
        /// 消息类型text
        /// </summary>
        public override WeiXinMsgType WeiXinMsgType
        {
            get { return WeiXinMsgType.Text; }
        }
        #endregion

        public override void ProcessWeiXin(WeiXinContext context)
        {
            WeiXinTextMessageEntity requestModel = context.Request.GetRequestModel<WeiXinTextMessageEntity>();

            WeiXinTextMessageEntity responseModel = new WeiXinTextMessageEntity
                {
                    ToUserName =requestModel.FromUserName,
                    Content = string.Format("你请求的是text类型消息!执行的控制器是:{0},实现:{1}", this.GetType().FullName, this.GetType().GetInterface("IWeiXinHandler").FullName),
                    MsgType = requestModel.MsgType
                };
            context.Response.Write(responseModel);
        }
    }
}

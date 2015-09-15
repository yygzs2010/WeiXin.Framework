using System.Xml.Linq;
using WeiXin.Framework.Core;
using WeiXin.Framework.Core.Entities;

namespace WeiXin.Framework.Plugins
{
    /// <summary>
    /// 微信图片消息处理控制器
    /// </summary>
    public class WeiXinImageHandler:WeiXinHandlerType
    {
        #region 字段/属性

        /// <summary>
        /// 消息类型image
        /// </summary>
        public override WeiXinMsgType WeiXinMsgType
        {
            get { return WeiXinMsgType.Image; }
        }

        #endregion

        public override void ProcessWeiXin(WeiXinContext context)
        {
            WeiXinImageMessageEntity requestModel = context.Request.GetRequestModel<WeiXinImageMessageEntity>();

            WeiXinImageMessageEntity responseModel = new WeiXinImageMessageEntity
                {
                    ToUserName = requestModel.FromUserName,
                    MsgType = WeiXinMsgType.Image.ToString().ToLower()
                };

            XElement xElement = responseModel.GetXElement();
            xElement.Add(new XElement("Image", new XElement("MediaId", requestModel.MediaId)));

            context.Response.Write(xElement);
        }
    }
}

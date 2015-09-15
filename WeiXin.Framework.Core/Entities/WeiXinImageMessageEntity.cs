namespace WeiXin.Framework.Core.Entities
{
    /// <summary>
    /// 图片消息实体类
    /// </summary>
    public class WeiXinImageMessageEntity : WeiXinMessageBaseEntity
    {
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 图片消息媒体id
        /// </summary>
        public string MediaId { get; set; }
    }
}

namespace WeiXin.Framework.Core.Entities
{
    /// <summary>
    /// 文本消息实体类
    /// </summary>
    public class WeiXinTextMessageEntity:WeiXinMessageBaseEntity
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
    }
}

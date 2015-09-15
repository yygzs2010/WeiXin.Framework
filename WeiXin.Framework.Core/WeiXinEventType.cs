namespace WeiXin.Framework.Core
{
    /// <summary>
    /// 微信事件消息类型
    /// </summary>
    public enum WeiXinEventType
    {
        /// <summary>
        /// 事件默认值，代表不是事件消息
        /// </summary>
        None=0,
        /// <summary>
        /// 用户关注事件消息
        /// </summary>
        SubScribe,
        /// <summary>
        /// 用户取消关注事件消息
        /// </summary>
        UnSubScribe,
        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        Location,
        /// <summary>
        /// 用户已关注时的事件推送
        /// </summary>
        Scan,
        /// <summary>
        /// 点击菜单拉取消息时的事件推送
        /// </summary>
        Click,
        /// <summary>
        /// 点击菜单跳转链接时的事件推送
        /// </summary>
        View
    }
}

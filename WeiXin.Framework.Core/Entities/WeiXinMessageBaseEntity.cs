using System;
using System.Configuration;
using System.Globalization;

namespace WeiXin.Framework.Core.Entities
{
    /// <summary>
    /// 微信消息基类
    /// </summary>
    public abstract class WeiXinMessageBaseEntity
    {
        /// <summary>
        /// 接收方
        /// </summary>
        public virtual string ToUserName { get; set; }

        private string _fromUserName;
        /// <summary>
        /// 发送方
        /// </summary>
        public virtual string FromUserName
        {
            get
            {
                if (string.IsNullOrEmpty(this._fromUserName))
                {
                    this._fromUserName = ConfigurationManager.ConnectionStrings["WeiXinAccoutName"].ConnectionString;
                }
                return this._fromUserName;
            }
            set { this._fromUserName = value; }
        }

        private string _createTime;
        /// <summary>
        /// 消息创建时间
        /// </summary>
        public virtual string CreateTime
        {
            get
            {
                if (string.IsNullOrEmpty(this._createTime))
                {
                    this._createTime = DateTime.Now.GetInt().ToString(CultureInfo.InvariantCulture);
                }
                return this._createTime;
            }
            set { this._createTime = value; }
        }
        /// <summary>
        /// 消息类型
        /// </summary>
        public virtual string MsgType { get; set; }
        /// <summary>
        /// 消息事件类型
        /// </summary>
        public virtual string Event { get; set; }
        /// <summary>
        /// 消息ID(64位整型)
        /// </summary>
        public virtual string MsgId { get; set; }
    }
}

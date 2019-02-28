using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    /// <summary>
    /// 同步消息实体
    /// </summary>
    public class NotifyPhoneEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseRequestEntity BaseRequest { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClientMsgId { get; set; }
    }
}

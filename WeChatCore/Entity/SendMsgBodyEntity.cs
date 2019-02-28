using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    /// <summary>
    /// 发送消息实体
    /// </summary>
    public class SendMsgBodyEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseRequestEntity BaseRequest { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SendMsgEntity Msg { get; set; }
    }
}

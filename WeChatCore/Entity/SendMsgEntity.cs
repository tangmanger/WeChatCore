using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatCore.Enum;

namespace WeChatCore.Entity
{
    public class SendMsgEntity
    {
            /// <summary>
            /// 
            /// </summary>
            public MsgTypeEnum Type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Content { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string FromUserName { get; set; }
            /// <summary>
            /// (string.IsNullOrWhiteSpace(ToName) ? ContactsThis.UserName : ToName) //接受人名称
            /// </summary>
            public string ToUserName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string LocalID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ClientMsgId { get; set; }
        }
}

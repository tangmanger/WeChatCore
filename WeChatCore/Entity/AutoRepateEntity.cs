using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatCore.Enum;

namespace WeChatCore.Entity
{
    /// <summary>
    /// 自动回复实体
    /// </summary>
    public class AutoRepateEntity
    {
        /// <summary>
        /// 自动回复类型
        /// </summary>
        public AutoRepateEnum AutoRepateType { get; set; }
        /// <summary>
        /// 自动回复开关
        /// </summary>
        public string AutoReplySwitch { get; set; }
        /// <summary>
        /// 自动回复内容
        /// </summary>
        public string ReplyContent { get; set; }
    }
}

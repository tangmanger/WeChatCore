using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Enum
{
    /// <summary>
    /// 联系人类型
    /// </summary>
    public enum ContactType
    {
        /// <summary>
        /// 公众号等
        /// </summary>
        PublicContact = 0,
        /// <summary>
        /// 联系人
        /// </summary>
        PersonContact = 1,
        /// <summary>
        /// 微信群
        /// </summary>
        GroupContact = 2
    }
}

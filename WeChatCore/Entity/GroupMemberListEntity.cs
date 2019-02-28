using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    /// <summary>
    /// 群成员列表
    /// </summary>
    public class GroupMemberListEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseResponseEntity BaseResponse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MemberListItemEntity> ContactList { get; set; }
    }
}

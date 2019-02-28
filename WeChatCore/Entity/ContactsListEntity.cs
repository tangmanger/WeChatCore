using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    public class ContactsListEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseResponseEntity BaseResponse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MemberCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MemberListItemEntity> MemberList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Seq { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    public class GroupMemberEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int Uin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long AttrStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PYInitial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PYQuanPin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RemarkPYInitial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RemarkPYQuanPin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MemberStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string KeyWord { get; set; }
    }
}

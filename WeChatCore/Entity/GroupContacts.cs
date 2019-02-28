using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    public class GroupListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        private string _EncryChatRoomId = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string EncryChatRoomId
        {
            get
            {
                return _EncryChatRoomId;
            }
            set
            {
                _EncryChatRoomId = value;
            }
        }
    }
    public class GroupContacts
    {
      
            /// <summary>
            /// 
            /// </summary>
            public BaseRequestEntity BaseRequest { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int Count { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<GroupListItem> List { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    /// <summary>
    /// 收到的消息实体
    /// </summary>
    public class ReceiveMsgEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseResponseEntity BaseResponse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AddMsgCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<AddMsgListItemEntity> AddMsgList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ModContactCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ModContactList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DelContactCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> DelContactList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ModChatRoomMemberCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ModChatRoomMemberList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ProfileEntity Profile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ContinueFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SyncKeyEntity SyncKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SyncKeyEntity SyncCheckKey { get; set; }
    }
}

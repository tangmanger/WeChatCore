using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    public class BaseContactEntity
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
        public List<ContactListItemEntity> ContactList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SyncKeyEntity SyncKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserEntity User { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ChatSet { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ClientVersion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SystemTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int GrayScale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int InviteStartCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MPSubscribeMsgCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MPSubscribeMsgListItemEntity> MPSubscribeMsgList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ClickReportInterval { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    public class ContactListItemEntity
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
        /// 文件传输助手
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ContactFlag { get; set; }
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
        public string RemarkName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int HideInputBarFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int VerifyFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int OwnerUin { get; set; }
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
        public int StarFriend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AppAccountFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Statues { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long AttrStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SnsFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UniFriend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ChatRoomId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EncryChatRoomId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsOwner { get; set; }
    }
}

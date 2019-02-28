using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    public class ProfileEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int BitFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserNameEntity UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public NickNameEntity NickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int BindUin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BindEmailEntity BindEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BindMobileEntity BindMobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PersonalCard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int HeadImgUpdateFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Signature { get; set; }
    }

}

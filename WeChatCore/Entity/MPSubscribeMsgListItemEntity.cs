using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    public class MPSubscribeMsgListItemEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MPArticleCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MPArticleListItemEntity> MPArticleList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 算法与数据结构
        /// </summary>
        public string NickName { get; set; }
    }
}

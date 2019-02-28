using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    public class SendFileResultEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseResponseEntity BaseResponse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int StartPos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CDNThumbImgHeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CDNThumbImgWidth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EncryFileName { get; set; }
    }
}

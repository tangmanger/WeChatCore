using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    /// <summary>
    /// 发送消息返回结果
    /// </summary>
    public class SendMsgResultEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseResponseEntity BaseResponse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MsgID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LocalID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    /// <summary>
    /// 基本请求构造实体
    /// </summary>
    public class BaseRequestEntity
    {
      
        /// <summary>
        /// Uin
        /// </summary>
        public string Uin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Sid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Skey { get; set; }
        /// <summary>
        /// 
        /// </summary> 
        public string DeviceID { get; set; }
    }
    public class BaseRequestSubmitEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseRequestEntity BaseRequest { get; set; }
    }
}

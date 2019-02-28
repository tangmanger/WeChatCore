using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeChatCore.Entity
{
    /// <summary>
    /// 登录结果实体
    /// </summary>
    [XmlRoot("error")]
    public class LoginResultEntity
    {
        /// <summary>
        /// code
        /// </summary>
        public string ret { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// sky值
        /// </summary>
        public string skey { get; set; }
        /// <summary>
        /// 微信ID
        /// </summary>
        public string wxsid { get; set; }
        /// <summary>
        /// 微信UIN
        /// </summary>
        public string wxuin { get; set; }
        /// <summary>
        /// 微信凭证
        /// </summary>
        public string pass_ticket { get; set; }
        /// <summary>
        /// unknown
        /// </summary>
        public string isgrayscale { get; set; }
    }
}

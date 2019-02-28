using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    public class BaseResponseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int Ret { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrMsg { get; set; }
    }
}

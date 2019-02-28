using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatCore.Enum;

namespace WeChatCore.Entity
{
    public class FileEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int UploadType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BaseRequestEntity BaseRequest { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClientMediaId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long TotalLen { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long StartPos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long DataLen { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MediaTypeEnum MediaType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FileMd5 { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        [JsonIgnore]
        public string FileName { get; set; }
        /// <summary>
        /// 文件修改时间
        /// </summary>
        [JsonIgnore]
        public DateTime FileModiftTime { get; set; }
        /// <summary>
        /// 传输的文件类型(image/jpeg)
        /// </summary>
        [JsonIgnore]
        public string FileType { get; set; }
        /// <summary>
        /// 媒体类型（pic）
        /// </summary>
        [JsonIgnore]
        public string Mediatype1 { get; set; }
    }
}

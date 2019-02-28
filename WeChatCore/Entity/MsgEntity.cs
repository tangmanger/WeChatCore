using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Newtonsoft.Json;
using WeChatCore.Enum;

namespace WeChatCore.Entity
{
    /// <summary>
    /// 消息实体
    /// </summary>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MsgEntity
    {
        /// <summary>
        /// 消息来源
        /// </summary>
        public MsgOwerTypeEnum MsgOwerType { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgTypeEnum MsgType { get; set; }
        /// <summary>
        /// 消息时间
        /// </summary>
        public DateTime MsgTime { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public object MsgContent { get; set; }
        /// <summary>
        /// 消息所属人员
        /// </summary>
        public MemberListItemEntity MsgOwer { get; set; }
        /// <summary>
        /// （非文本消息的文件名称）
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 文件路径(非文本消息)
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 文件缓存路径
        /// </summary>
        public string FileTempPath { get; set; }
        /// <summary>
        /// 消息链接（比如地图消息之类的链接）
        /// </summary>
        public string MsgUrl { get; set; }
        /// <summary>
        /// 如果是群成员这个是群成员的具体谁
        /// </summary>
        public GroupMemberEntity GroupMember { get; set; }
        /// <summary>
        /// 消息是否已读
        /// </summary>
        public bool HasRead { get; set; }
        private FlowDocument _Document = new FlowDocument();
        /// <summary>
        /// 流文档显示
        /// </summary>
        [JsonIgnore]
        public FlowDocument Document
        {
            get
            {
                return _Document;
            }
            set
            {
                _Document = value;
            }
        }
        /// <summary>
        /// 是否设置自动回复
        /// </summary>
        public bool IsCanAutoReply { get; set; }

    }
}

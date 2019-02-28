using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Entity
{
    public class AddMsgListItemEntity
    {
        /// <summary>
        /// 消息id
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 消息来源
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 发送目标
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public int MsgType { get; set; }
        /// <summary>
        /// 来来来
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 图片状态
        /// </summary>
        public int ImgStatus { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public int CreateTime { get; set; }
        /// <summary>
        /// 语音长度
        /// </summary>
        public int VoiceLength { get; set; }
        /// <summary>
        /// 播放长度
        /// </summary>
        public int PlayLength { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }
        /// <summary>
        /// 媒体ID
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// MAP消息URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 公众号、小程序消息类型
        /// </summary>
        public int AppMsgType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int StatusNotifyCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StatusNotifyUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RecommendInfoEntity RecommendInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ForwardFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AppInfoEntity AppInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int HasProductId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Ticket { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ImgHeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ImgWidth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SubMsgType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long NewMsgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OriContent { get; set; }
    }
}

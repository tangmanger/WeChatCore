using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Enum
{
    //  1  文本消息
    //3  图片消息
    //34 语音消息
    //37 VERIFYMSG
    //40 POSSIBLEFRIEND_MSG
    //42 共享名片
    //43 视频通话消息
    //47 动画表情
    //48 位置消息
    //49 分享链接
    //50 VOIPMSG
    //51 微信初始化消息
    //52 VOIPNOTIFY
    //53 VOIPINVITE
    //62 小视频
    //9999  SYSNOTICE
    //10000  系统消息
    //10002  撤回消息
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MsgTypeEnum
    {
        /// <summary>
        /// 文字消息
        /// </summary>
        Text = 1,
        /// <summary>
        /// 图片消息
        /// </summary>
        Picture = 3,
        /// <summary>
        /// 语音
        /// </summary>
        Voice = 34,
        /// <summary>
        /// Gif消息
        /// </summary>
        Gif = 49,
        /// <summary>
        /// 视频消息
        /// </summary>
        Video = 43,
        /// <summary>
        /// 地图消息
        /// </summary>
        Map = 99999,
        /// <summary>
        /// 系统消息
        /// </summary>
        SystemMsg = 10000
    }
}

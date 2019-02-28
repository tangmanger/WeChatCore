using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.DefineClass
{
    /// <summary>
    /// 路径文件
    /// </summary>
    public class DirectoryDefine
    {
        static string SystemPath = Environment.CurrentDirectory;
        /// <summary>
        /// 语音消息储存路径
        /// </summary>
        public static string VoiceMsgPath = "Data\\Voice";
        /// <summary>
        /// 视频路径
        /// </summary>
        public static string VideoMsgPath = "Data\\Video";
        /// <summary>
        /// 图片路径
        /// </summary>
        public static string ImageMsgPath = "Data\\Image";
        /// <summary>
        /// 小图片路径
        /// </summary>
        public static string ImageMsgTempPath
        {
            get
            {
                if (!Directory.Exists(SystemPath + "\\Data\\Image\\Temp"))
                {
                    Directory.CreateDirectory(SystemPath + "\\Data\\Image\\Temp");
                }
                return "Data\\Image\\Temp";
            }
        }
        /// <summary>
        /// 头像路径
        /// </summary>
        public static string HeaderImagePath = "Data\\HeaderImage";
        /// <summary>
        /// 动画路径
        /// </summary>
        public static string ImageMsgGifPath = "Data\\Gif";
        /// <summary>
        /// 地图消息
        /// </summary>
        public static string MapImageMsgPath = "Data\\MapImage";
    }
}

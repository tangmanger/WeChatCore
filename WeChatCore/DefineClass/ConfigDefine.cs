using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.DefineClass
{
    /// <summary>
    /// 配置路径
    /// </summary>
    public class ConfigDefine
    {
        /// <summary>
        /// 获取根路径
        /// </summary>
        /// <returns></returns>
        public static string GetLogDirectory()
        {
            string DirectoryPath = Environment.CurrentDirectory + "\\Config";
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);
            return DirectoryPath;
        }
        /// <summary>
        /// 自动回复配置文件
        /// </summary>
        public static string WeChatAutoReplyPath
        {
            get
            {
                return GetLogDirectory() + "\\AutoReply.conf";
            }
        }
    }
}

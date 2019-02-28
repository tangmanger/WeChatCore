using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.DefineClass
{
    public class LogPathDefine
    {
        /// <summary>
        /// 获取根路径
        /// </summary>
        /// <returns></returns>
        public static string GetLogDirectory()
        {
            string DirectoryPath = Environment.CurrentDirectory + "\\Log";
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);
            return DirectoryPath;
        }
        /// <summary>
        /// 系统日志
        /// </summary>
        public static string SystemLogPath
        {
            get
            {
                return GetLogDirectory() + "\\System.log";
            }
        }
        /// <summary>
        /// 微信正常日志
        /// </summary>
        public static string WeChatLogPath
        {
            get
            {
                return GetLogDirectory() + "\\WeChat.log";
            }
        }
        /// <summary>
        /// 微信正常日志
        /// </summary>
        public static string WeChatErrorLogPath
        {
            get
            {
                return GetLogDirectory() + "\\WeChatError.log";
            }
        }
        /// <summary>
        /// 系统异常日志
        /// </summary>
        public static string ExceptionLogPath
        {
            get
            {
                return GetLogDirectory() + "\\Exception.log";
            }
        }
        /// <summary>
        /// 微信群日志
        /// </summary>
        public static string WeChatGrouplogPath
        {
            get
            {
                return GetLogDirectory() + "\\WeChatGroup.log";
            }
        }
    }
}

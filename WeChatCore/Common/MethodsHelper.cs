using CommonTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WeChatCore.DefineClass;

namespace WeChatCore.Common
{
    /// <summary>
    /// 方法合集
    /// </summary>
    public static class MethodsHelper
    {
        /// <summary>
        /// 返回ClientMsgId
        /// </summary>
        /// <returns></returns>
        public static string GetClientMsgId()
        {
            string UnixTimeSpan = DateTimeToosHelper.GetUnixTimeSpan().ToString();
            // UnixTimeSpan = UnixTimeSpan.Substring(0, 4) + GetThreeNumber() + new Random().Next(0, 9);
            return UnixTimeSpan + GetThreeNumber() + GetThreeNumber() + new Random().Next(0, 9);
        }
        /// <summary>
        /// 返回三位随机数
        /// </summary>
        /// <returns></returns>
        public static string GetThreeNumber()
        {
            Random r = new Random();
            StringBuilder sb = new StringBuilder();
            int i = r.Next(0, 9);
            sb.Append(i);
            int j = r.Next(i, 9);
            sb.Append(j);
            int k = r.Next(j, 9);
            sb.Append(k);
            return sb.ToString();
        }
        public static string GetDeviceId()
        {//DeviceID=e3244 3631 0531 059
            Random r = new Random();
            StringBuilder sb = new StringBuilder();
            int i = r.Next(0, 9);
            sb.Append(i);
            int j = r.Next(i, 9);
            sb.Append(j);
            int k = r.Next(j, 9);
            sb.Append(k);

            i = r.Next(k, 9);
            sb.Append(i);
            j = r.Next(i, 9);
            sb.Append(j);
            k = r.Next(j, 9);
            sb.Append(k);

            i = r.Next(k, 9);
            sb.Append(i);
            j = r.Next(i, 9);
            sb.Append(j);
            k = r.Next(j, 9);
            sb.Append(k);
            sb.Append(GetThreeNumber());
            return "e" + sb.ToString();
        }
        /// <summary>
        /// 生成MD5
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncryptWithMD5(string source)
        {
            byte[] sor = Encoding.UTF8.GetBytes(source);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strbul = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                strbul.Append(result[i].ToString("x2"));//加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位

            }
            return strbul.ToString();
        }
        /// <summary>
        /// 下载头像的保存
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static string HeadImageSaveFile(string FilePath,string NickName)
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\" + FilePath+"\\"+ EncryptWithMD5(CommonDefine.BaseContact.User.NickName)))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + FilePath + "\\" + EncryptWithMD5(CommonDefine.BaseContact.User.NickName));
            return EncryptWithMD5(NickName);
        }
        /// <summary>
        /// 判断当前路径是否存在，不存在生成路径并返回一个UUID的文件名
        /// </summary>
        /// <param name="UserName">当前用户名</param>
        /// <returns>返回文件是UUID</returns>
        public static string MsgSaveFile(string FilePath)
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\" + FilePath))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + FilePath);
            //    byte[] message = Convert.FromBase64String(Base64Code);
            string Guids = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            //string FilePath = Environment.CurrentDirectory + "\\" + UserName + "\\" + Guids + ".mp3";
            return Guids;
        }
    }
}

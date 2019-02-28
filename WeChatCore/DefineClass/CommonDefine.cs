using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WeChatCore.Entity;

namespace WeChatCore.DefineClass
{
    public static class CommonDefine
    {
        /// <summary>
        /// Cookie
        /// </summary>
        public static System.Net.CookieContainer Cookies { get; set; }
        /// <summary>
        /// 个人头像
        /// </summary>
        public static BitmapImage UserImage { get; set; }
        /// <summary>
        /// 个人头像
        /// </summary>
        public static string Base64 { get; set; }
        /// <summary>
        /// 登录结果
        /// </summary>
        public static LoginResultEntity LoginResult { get; set; }
        /// <summary>
        /// 最近联系人包含自己个人信息
        /// </summary>
        public static BaseContactEntity BaseContact { get; set; }
        /// <summary>
        /// 同步Key
        /// </summary>
        public static SyncKeyEntity SyncKey { get; set; }
        /// <summary>
        /// 联系人列表
        /// </summary>
        public static ContactsListEntity ContactsList { get; set; }
        /// <summary>
        /// 群成员列表
        /// </summary>
        public static GroupMemberListEntity GroupMemberList { get; set; }
        /// <summary>
        /// 同步Key获取消息用
        /// </summary>
        public static string GetMsgSyncKey { get; set; }
        /// <summary>
        /// 获取set-cookie
        /// </summary>
        public static Dictionary<string, string> GetCookieDictionary = new Dictionary<string, string>();
        /// <summary>
        /// 白名单
        /// </summary>
        public static List<MemberListItemEntity> WhiteUserList { get; set; }
    }
}

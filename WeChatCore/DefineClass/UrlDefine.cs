using CommonTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatCore.Common;

namespace WeChatCore.DefineClass
{
    /// <summary>
    /// Url定义
    /// </summary>
    public static class UrlDefine
    {
        /// <summary>
        /// Loginurl
        /// </summary>
        public static string LoginUrls { get; set; }
        /// <summary>
        /// 唯一对应UUID
        /// </summary>
        public static string Tickets { get; set; }
        /// <summary>
        /// 网站主目录
        /// </summary>
        public static string RootUrl = "https://wx.qq.com";
        /// <summary>
        /// 登录url 返回值:window.QRLogin.code = 200; window.QRLogin.uuid = "Yc4SuTCdqw==";
        /// </summary>
        public static string LoginUrl = "https://login.weixin.qq.com/jslogin?appid=wx782c26e4c19acffb&redirect_uri=https%3A%2F%2Flogin.weixin.qq.com%2Fcgi-bin%2Fmmwebwx-bin%2Fwebwxnewloginpage&fun=new&lang=zh_CN&_=1455781223306";
        /// <summary>
        /// 获得Token的url(验证码图片）
        /// </summary>
        public static string LoginUrlWithToken = "https://login.weixin.qq.com/qrcode/";
        /// <summary>
        /// 等待Url
        /// </summary>
        public static string HoldOnUrl = "https://login.wx.qq.com/cgi-bin/mmwebwx-bin/login?loginicon=true&uuid=";// + Tickets + "&tip=0&r=1499867820&_=1497443441656";
        /// <summary>
        /// 获取登录地址
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public static string WaitingUrl(string uuid)
        {
            return HoldOnUrl + uuid + "&tip=0&r=" + DateTimeToosHelper.GetUnixTimeSpan().ToString() + "& _=" + DateTimeToosHelper.GetUnixTimeSpan().ToString() + MethodsHelper.GetThreeNumber();
        }
        /// <summary>
        /// 获取个人信息
        /// </summary>
        public static string GetLoginId = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxinit?r=" + DateTimeToosHelper.GetUnixTimeSpan().ToString() + "&lang=zh_CN&pass_ticket=";
        /// <summary>
        /// 获取联系人
        /// </summary>
        public static string ContactUrl = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetcontact?pass_ticket=";
        /// <summary>
        /// 聊天url
        /// </summary>
        public static string ChatUrl = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxsendmsg?pass_ticket=";
        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="tickets"></param>
        /// <param name="skey"></param>
        /// <returns></returns>
        public static string GetContactUrl(string tickets, string skey)
        {
            return ContactUrl + tickets + "&r=" + DateTimeToosHelper.GetUnixTimeSpan().ToString() + "& seq=0&skey=" + skey;
        }
        /// <summary>
        /// 获取SyncKey后加参数Pass_ticket
        /// </summary>
        public static string GetSyncKey = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxinit?r=-" + DateTimeToosHelper.GetUnixTimeSpan().ToString() + "&pass_ticket=";
        /// <summary>
        /// 心跳url
        /// </summary>
        public static string HeartUrl = "https://webpush.wx.qq.com/cgi-bin/mmwebwx-bin/synccheck?";
        /// <summary>
        /// 获取信息url
        /// </summary>
        public static string GetChatUrl = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxsync?";
        /// <summary>
        /// 获取语音消息
        /// </summary>
        public static string VoiceUrl = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetvoice?msgid={0}&skey={1}";
        /// <summary>
        /// 获取图片信息
        /// </summary>
        public static string ImgUrl = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetmsgimg?&MsgID={0}&skey={1}&type=slave";
        /// <summary>
        /// 大图
        /// </summary>
        public static string ImgUrlBig = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetmsgimg?&MsgID={0}&skey={1}";
        /// <summary>
        /// 视频地址
        /// </summary>
        public static string VideoUrl = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetvideo?msgid={0}&skey={1}";
        /// <summary>
        /// 获取地图图片
        /// </summary>
        public static string MapUrl = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetpubliclinkimg?url=xxx&msgid={0}&pictype=location";
        /// <summary>
        /// 获取群成员信息
        /// </summary>
        public static string GetQunContactUrl = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxbatchgetcontact?type=ex&r=" + DateTimeToosHelper.GetUnixTimeSpan().ToString() + MethodsHelper.GetThreeNumber() + "&pass_ticket={0}";
        /// <summary>
        /// 与手机的心跳keep
        /// </summary>
        public static string WexNotifyUrl = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxstatusnotify?pass_ticket=";
        /// <summary>
        /// 发送文件
        /// </summary>
        public static string SendFileUrl = "https://file.wx.qq.com/cgi-bin/mmwebwx-bin/webwxuploadmedia?f=json";
    }
}

using CommonTools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WeChatCore.Common;
using WeChatCore.DefineClass;

namespace WeChatCore.Entity
{
    public class MemberListItemEntity : NotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public int Uin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long AttrStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PYInitial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PYQuanPin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RemarkPYInitial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RemarkPYQuanPin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MemberStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ContactFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MemberCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<GroupMemberEntity> MemberList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RemarkName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int HideInputBarFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int VerifyFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int OwnerUin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StarFriend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AppAccountFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Statues { get; set; }
        /// <summary>
        /// 河北
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 唐山
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SnsFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UniFriend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ChatRoomId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EncryChatRoomId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsOwner { get; set; }
        private string _DisplayNameDef = string.Empty;
        [JsonIgnore]
        public string DisplayNameDef
        {
            get
            {
                if (string.IsNullOrWhiteSpace(RemarkName))
                    return NickName;
                else
                    return RemarkName + "(" + NickName + ")";
            }
            set
            {
                _DisplayNameDef = value;
                RaisePropertyChanged(() => DisplayNameDef);
            }
        }
        private string _HeadUrlDef = string.Empty;
        [JsonIgnore]
        public string HeadUrlDef
        {
            get
            {
                return _HeadUrlDef;
            }
            set
            {
                _HeadUrlDef = value;
                RaisePropertyChanged(() => HeadUrlDef);
                RaisePropertyChanged(() => ImageSource);
            }
        }
        private List<MsgEntity> _MsgList = new List<MsgEntity>();
        /// <summary>
        /// 消息贮存
        /// </summary>
        [JsonIgnore]
        public List<MsgEntity> MsgList
        {
            get
            {
                return _MsgList;
            }
            set
            {
                _MsgList = value;
            }
        }
        /// <summary>
        /// 能否自动回复
        /// </summary>
        public bool IsCanAutoReply { get; set; }
        private int _MsgCount = 0;
        /// <summary>
        /// 消息数量
        /// </summary>
        [JsonIgnore]
        public int MsgCount
        {
            get
            {
                return _MsgCount;
            }
            set
            {
                _MsgCount = value;
            }
        }
        public ImageSource ImageSource
        {
            get
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(HeadUrlDef))
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/WeChatCore;component/Resource/DefultHeader.png"));
                    }
                    return new BitmapImage(new Uri(HeadUrlDef));
                }
                catch (Exception ex)
                {
                    LogWriter.Write("设置头像发生异常" + DisplayNameDef, LogPathDefine.ExceptionLogPath);
                    try
                    {
                        File.SetAttributes(HeadUrlDef, FileAttributes.Normal);
                        File.Delete(HeadUrlDef);
                        DownloadImage();
                    }
                    catch (Exception e)
                    {
                        LogWriter.Write("设置头像发生异常" + e.Message, LogPathDefine.ExceptionLogPath);
                    }
                    return new BitmapImage(new Uri("pack://application:,,,/WeChatCore;component/Resource/DefultHeader.png"));
                }
            }
        }
        public void DownloadImage()
        {
            string uuid = MethodsHelper.HeadImageSaveFile(DirectoryDefine.HeaderImagePath, DisplayNameDef);
            if (File.Exists(Environment.CurrentDirectory + "\\"+DirectoryDefine.HeaderImagePath + "\\" + MethodsHelper.EncryptWithMD5(CommonDefine.BaseContact.User.NickName) + uuid + ".jpg"))
            {
                HeadUrlDef = Environment.CurrentDirectory + "\\" + DirectoryDefine.HeaderImagePath + "\\" + MethodsHelper.EncryptWithMD5(CommonDefine.BaseContact.User.NickName) + uuid + ".jpg";
                return;
            }
            if (string.IsNullOrWhiteSpace(HeadUrlDef))
            {

                HttpHelper.HttpMethods.GetFile(UrlDefine.RootUrl + HeadImgUrl, Environment.CurrentDirectory + "\\" + DirectoryDefine.HeaderImagePath + "\\" + MethodsHelper.EncryptWithMD5(CommonDefine.BaseContact.User.NickName) + uuid + ".jpg", CommonDefine.Cookies);
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    FileInfo fileInfo = new FileInfo(Environment.CurrentDirectory + "\\" + DirectoryDefine.HeaderImagePath + "\\" + MethodsHelper.EncryptWithMD5(CommonDefine.BaseContact.User.NickName) + uuid + ".jpg");
                    if (fileInfo.Length == 0)
                        HeadUrlDef = "pack://application:,,,/WeChatCore;component/Resource/DefultHeader.png";
                    else
                        HeadUrlDef = Environment.CurrentDirectory + "\\" + DirectoryDefine.HeaderImagePath + "\\" + MethodsHelper.EncryptWithMD5(CommonDefine.BaseContact.User.NickName) + uuid + ".jpg";
                }));
            }
            else
            {
                LogWriter.Write(string.Format("当前人员{0}已有头像", DisplayNameDef), LogPathDefine.WeChatLogPath);
            }
        }

    }
}

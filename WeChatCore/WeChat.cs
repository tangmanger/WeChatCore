using CommonTools;
using HttpHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Media.Imaging;
using WeChatCore.Common;
using WeChatCore.DefineClass;
using WeChatCore.Entity;
using WeChatCore.Enum;

namespace WeChatCore
{
    /// <summary>
    /// weichat
    /// </summary>
    public static class WeChat
    {
        #region 初始化微信机器人

        /// <summary>
        /// 初始化消息机器人 注：必须用异步线程，否则会阻塞主线程
        /// </summary>
        public static void InitWeChatRobot()
        {
            bool DownLoadImageResult = DownloadImage();
            if (DownLoadImageResult == true)
            {
                CommonMethodCallBackHandlers.OnDownLoadQRCodeCompleted();
                //下载成功
                Login();
                CommonDefine.GetMsgSyncKey = GetSyncKey();
                while (true)
                {
                    try
                    {
                        LogWriter.Write(string.Format("获取心跳信息..."), LogPathDefine.WeChatLogPath);
                        KeepHeart(CommonDefine.GetMsgSyncKey);
                    }
                    catch (Exception ex)
                    {
                        LogWriter.Write(string.Format(ex.Message), LogPathDefine.ExceptionLogPath);
                    }
                }
            }
            else
            {
                LogWriter.Write(string.Format("下载验证码失败，请重试..."), LogPathDefine.WeChatLogPath);
                throw new Exception("下载验证码失败，请重试...");
            }
        }

        #endregion

        #region 环境校验
        public static void CheckMethod()
        {

        }
        #endregion

        #region 下载验证码

        /// <summary>
        /// 下载验证码
        /// </summary>
        public static bool DownloadImage()
        {
            HttpResponseResult ResponseResult = HttpMethods.Get(UrlDefine.LoginUrl, new System.Net.CookieContainer(), Encoding.UTF8);
            if (ResponseResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string[] str = ResponseResult.ContentData.ToString().Split(';');
                string ImageCode = str[1].Substring(str[1].IndexOf("\"") + 1, str[1].Length - str[1].IndexOf("\"") - 2);
                UrlDefine.Tickets = ImageCode;
                CommonDefine.Cookies = HttpMethods.getCookie(UrlDefine.RootUrl);
                ResponseResult = HttpMethods.GetFile(UrlDefine.LoginUrlWithToken + ImageCode, "Check.jpg", new System.Net.CookieContainer());
                if (ResponseResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    LogWriter.Write("下载微信登录二维码成功...", LogPathDefine.WeChatLogPath);
                    return true;
                }
                else
                {
                    LogWriter.Write("获取微信登录二维码失败...", LogPathDefine.WeChatErrorLogPath);
                    LogWriter.Write(string.Format("当前返回状态码是{0},返回信息是{1}", ResponseResult.StatusCode.ToString(), ResponseResult.ReturnMsg), LogPathDefine.WeChatErrorLogPath);
                    return false;
                }
            }
            else
            {
                //请求出现问题
                LogWriter.Write("访问微信登官网失败...", LogPathDefine.WeChatErrorLogPath);
                LogWriter.Write(string.Format("当前返回状态码是{0},返回信息是{1}", ResponseResult.StatusCode.ToString(), ResponseResult.ReturnMsg), LogPathDefine.WeChatErrorLogPath);
                return false;
            }
        }

        #endregion

        #region 登录

        /// <summary>
        /// 登录
        /// </summary>
        public static void Login()
        {
            string str = string.Empty;
            HttpMethods.ContentType = "application/json;charset=utf-8";
            while (true)
            {
                if (!str.Contains("200"))
                {
                    HttpResponseResult ResponseResult = HttpMethods.Get(UrlDefine.WaitingUrl(UrlDefine.Tickets), CommonDefine.Cookies, Encoding.UTF8);
                    str = ResponseResult.ContentData.ToString();
                    Console.WriteLine("当前返回值是:" + str);
                    if (str.Contains("201"))
                    {
                        //扫码登录中
                        string[] base64 = str.Split(',');
                        CommonDefine.Base64 = base64[1];
                        byte[] arr = Convert.FromBase64String(CommonDefine.Base64.Replace("'", "").Replace(";", "").Trim());
                        File.WriteAllBytes(Environment.CurrentDirectory + "\\User.jpg", arr);
                        MemoryStream ms = new MemoryStream(arr);
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = ms;// new FileStream();
                        bi.EndInit();
                        CommonDefine.UserImage = bi;
                        CommonMethodCallBackHandlers.OnLoginScranQRCodeCompleted(Environment.CurrentDirectory + "\\User.jpg");
                    }
                }
                if (str.Contains("200"))
                {
                    //扫码登录完成
                    LogWriter.Write("扫码登录成功...", LogPathDefine.WeChatLogPath);
                    string[] strs = str.Split(';');
                    UrlDefine.LoginUrls = strs[1].Substring(strs[1].IndexOf("=\"") + 2, strs[1].Length - strs[1].IndexOf("=\"") - 3);
                    string XmlList = HttpMethods.Get(UrlDefine.LoginUrls, CommonDefine.Cookies, Encoding.UTF8, true).ContentData.ToString();
                    CommonDefine.GetCookieDictionary = HttpMethods.SetCookieDictionary;
                    CommonDefine.LoginResult = XMLHelper.DESerializer<LoginResultEntity>(XmlList);
                    //此处可用于二次点击登录
                    LogWriter.Write(CommonDefine.LoginResult.wxuin, Environment.CurrentDirectory + "\\Data\\data.dat");
                    Random r = new Random();
                    BaseRequestEntity Bre = new BaseRequestEntity() { Uin = CommonDefine.LoginResult.wxuin, Sid = CommonDefine.LoginResult.wxsid, Skey = CommonDefine.LoginResult.skey, DeviceID = MethodsHelper.GetDeviceId() };
                    BaseRequestSubmitEntity le = new BaseRequestSubmitEntity() { BaseRequest = Bre };
                    string postdata = JsonConvert.SerializeObject(le);
                    string MySelfList = HttpMethods.PostData(UrlDefine.GetLoginId + CommonDefine.LoginResult.pass_ticket, postdata, CommonDefine.Cookies, Encoding.UTF8).ContentData.ToString();
                    LogWriter.Write(string.Format("获取个人信息{0}...", MySelfList), LogPathDefine.WeChatLogPath);
                    CommonDefine.BaseContact = JsonConvert.DeserializeObject<BaseContactEntity>(MySelfList);
                    string ContentList = HttpMethods.Get(UrlDefine.GetContactUrl(CommonDefine.LoginResult.pass_ticket, CommonDefine.LoginResult.skey), CommonDefine.Cookies, Encoding.UTF8).ContentData.ToString();
                    CommonDefine.ContactsList = JsonConvert.DeserializeObject<ContactsListEntity>(ContentList);
                    LogWriter.Write(string.Format("获取联系人信息{0}...", ContentList), LogPathDefine.WeChatLogPath);
                    new Task(() => { GetGroupContactsMethod(CommonDefine.ContactsList.MemberList); }).Start();
                    new Task(() =>
                    {
                        CommonDefine.ContactsList.MemberList.ForEach((p) =>
                        {
                            p.DownloadImage();
                        });
                    }).Start();
                    CommonMethodCallBackHandlers.OnLoginCompleted(true);
                    break;
                }
                Thread.Sleep(5000);
            }

        }

        #endregion

        #region 下载头像

        public static void DownloadHeaderImage(MemberListItemEntity Mli)
        {
            Mli.DownloadImage();
        }
        #endregion

        #region 获取联系人

        /// <summary>
        /// 刷新联系人
        /// </summary>
        public static void RefreshContacts()
        {
            string ContentList = HttpMethods.Get(UrlDefine.GetContactUrl(CommonDefine.LoginResult.pass_ticket, CommonDefine.LoginResult.skey), CommonDefine.Cookies, Encoding.UTF8).ContentData.ToString();
            ContactsListEntity ContactsLists = JsonConvert.DeserializeObject<ContactsListEntity>(ContentList);
            if (ContactsLists != CommonDefine.ContactsList)//联系人有更新，关键是看数量增加还是减少的
            {
                CommonDefine.ContactsList = ContactsLists;
            }
        }

        #endregion

        #region 联系人分类
        /// <summary>
        /// 分类
        /// </summary>
        /// <returns></returns>
        public static List<MemberListItemEntity> SetSplite(ContactType ct)
        {
            switch (ct)
            {
                case ContactType.PublicContact:
                    ///公众号
                    var PublicContact = from item in CommonDefine.ContactsList.MemberList
                                        where item.VerifyFlag == 8 ||
                                        item.VerifyFlag == 24 ||
                                        item.VerifyFlag == 56
                                        select item;
                    if (PublicContact != null)
                        return PublicContact.ToList();
                    else
                        return new List<MemberListItemEntity>();
                case ContactType.PersonContact:
                    //个人
                    var PersonList = from item in CommonDefine.ContactsList.MemberList where item.VerifyFlag == 0 && item.UserName.Contains("@") select item;
                    if (PersonList != null)
                        return PersonList.ToList();
                    else
                        return new List<MemberListItemEntity>();
                case ContactType.GroupContact:
                    //群组
                    var GroupList = from item in CommonDefine.ContactsList.MemberList where item.VerifyFlag == 0 && item.UserName.Contains("@@") select item;
                    if (GroupList != null)
                        return GroupList.ToList();
                    else
                        return new List<MemberListItemEntity>();
            }
            return new List<MemberListItemEntity>();
        }
        #endregion

        #region 获取启动心跳参数

        /// <summary>
        /// 获取心跳key
        /// </summary>
        /// <returns></returns>
        public static string GetSyncKey()
        {
            BaseRequestEntity Bre = new BaseRequestEntity() { Uin = CommonDefine.LoginResult.wxuin, Sid = CommonDefine.LoginResult.wxsid, Skey = CommonDefine.LoginResult.skey, DeviceID = MethodsHelper.GetDeviceId() };
            BaseRequestSubmitEntity le = new BaseRequestSubmitEntity() { BaseRequest = Bre };
            string SyncKeyData = JsonConvert.SerializeObject(le);
            string SyncKeyUrl = UrlDefine.GetSyncKey + CommonDefine.LoginResult.pass_ticket;
            string SyncKeyList = HttpMethods.PostData(SyncKeyUrl, SyncKeyData, CommonDefine.Cookies, Encoding.UTF8).ContentData.ToString();
            BaseContactEntity bce = JsonConvert.DeserializeObject<BaseContactEntity>(SyncKeyList);
            CommonDefine.SyncKey = bce.SyncKey;
            string SyncKey = string.Empty;
            for (int index = 0; index < bce.SyncKey.List.Count; index++)
            {
                if (!string.IsNullOrWhiteSpace(SyncKey))
                {
                    SyncKey = SyncKey + "|";
                }
                SyncKey = SyncKey + bce.SyncKey.List[index].Key + "_" + bce.SyncKey.List[index].Val;
            }
            Thread.Sleep(5000);
            return SyncKey;
        }

        #endregion

        #region 获取心跳

        /// <summary>
        /// 获取心跳
        /// </summary>
        /// <param name="SyncKey"></param>
        public static void KeepHeart(string SyncKey)
        {
            Random ran = new Random();
            string HeartUrl = UrlDefine.HeartUrl + "r=" + DateTimeToosHelper.GetUnixTimeSpan().ToString() + MethodsHelper.GetThreeNumber() +
                "&skey=" + CommonDefine.LoginResult.skey
              + "&sid=" + CommonDefine.LoginResult.wxsid
              + "&uin=" + CommonDefine.LoginResult.wxuin
              + "&deviceid=e9128931905052" + ((int)ran.Next(0, 9)).ToString() + ((int)(ran.Next(0, 8) + 1)).ToString() + "&synckey=" + SyncKey + "&_=" + DateTimeToosHelper.GetUnixTimeSpan().ToString() + MethodsHelper.GetThreeNumber();
            LogWriter.Write(SyncKey, @"SyncKey.log");
            string HeartList = string.Empty;
            try
            {
                HeartList = HttpMethods.Get(HeartUrl, CommonDefine.Cookies, Encoding.UTF8).ContentData.ToString();
            }
            catch (Exception ex)
            {
                // HeartList = HttpMethods.Get(HeartUrl, CommonDefine.Cookies, Encoding.UTF8).ContentData.ToString();
            }
            if (!string.IsNullOrWhiteSpace(HeartList))
            {
                LogWriter.Write(HeartList.ToString());
                if (HeartList.ToString() != "window.synccheck={retcode:\"0\",selector:\"0\"}")
                {
                    //   Thread.CurrentThread.Abort();
                    Console.WriteLine(HeartList.ToString());
                    GetMsg(CommonDefine.SyncKey);
                    CommonDefine.GetMsgSyncKey = GetSyncKey();// 理论上此处不需要赋值，如果收发消息出现问题，放开次注释
                }
                else if (HeartList.ToString() != "window.synccheck={retcode:\"0\",selector:\"7\"}")
                {
                    // GetMsgSyncKey = GetSyncKey();
                }
                Console.WriteLine(HeartList.ToString());
                Console.WriteLine("thK's while is breaked!" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));
            }
            else
            {
                // GetMsgSyncKey = GetSyncKey();
            }
            return;

        }

        #endregion

        #region 获取聊天信息

        /// <summary>
        /// 获取聊天信息
        /// </summary>
        /// <param name="SyncKeyList"></param>
        /// <returns></returns>
        public static string GetMsg(SyncKeyEntity SyncKey)
        {
            string ReturnKey = string.Empty;
            ReceiveMsgEntity Rme = new ReceiveMsgEntity();
            GetMsgEntiy Gme = new GetMsgEntiy() { BaseRequest = new BaseRequestEntity() { Uin = CommonDefine.LoginResult.wxuin, Sid = CommonDefine.LoginResult.wxsid, Skey = CommonDefine.LoginResult.skey, DeviceID = MethodsHelper.GetDeviceId() }, SyncKey = SyncKey, rr = -113317163 };
            string PostData = JsonConvert.SerializeObject(Gme);
            string posturl = UrlDefine.GetChatUrl
                + "sid=" + CommonDefine.LoginResult.wxsid
                + "&skey=" + CommonDefine.LoginResult.skey
                + "&pass_ticket=" + CommonDefine.LoginResult.pass_ticket;
            string Msglist = HttpMethods.PostData(posturl, PostData, CommonDefine.Cookies, Encoding.UTF8).ContentData.ToString();
            try
            {
                Rme = JsonConvert.DeserializeObject<ReceiveMsgEntity>(Msglist);
                CommonDefine.SyncKey = Rme.SyncKey;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetMsg" + ex.Message);
            }
            try
            {
                if (Rme.AddMsgCount != 0)
                {
                    //说明有消息
                    AnalyseMsg(Rme.AddMsgList);
                }
                else
                {
                    //到这 说明没有消息
                }

            }
            catch (Exception ex)
            {
                // KeepHeart(FristKey);
                Console.WriteLine("GetMsg is out Exception" + ex.Message);
            }
            return ReturnKey;
        }

        #endregion

        #region 获取群成员

        /// <summary>
        /// 获取群成员信息
        /// </summary>
        /// <param name="GroupList"></param>
        public static void GetGroupContactsMethod(List<MemberListItemEntity> GroupList)
        {
            GroupContacts GC = new GroupContacts();
            GC.BaseRequest = new BaseRequestEntity() { DeviceID = MethodsHelper.GetDeviceId(), Sid = CommonDefine.LoginResult.wxsid, Skey = CommonDefine.LoginResult.skey, Uin = CommonDefine.LoginResult.wxuin };
            List<GroupListItem> GroupNumberList = new List<GroupListItem>();
            for (int index = 0; index < GroupList.Count; index++)
            {
                if (GroupList[index].UserName.Contains("@@"))
                {
                    GroupListItem Groupitem = new GroupListItem();
                    Groupitem.UserName = GroupList[index].UserName;
                    GroupNumberList.Add(Groupitem);
                }
            }
            GC.List = GroupNumberList;
            GC.Count = GroupNumberList.Count;
            string PostData = JsonConvert.SerializeObject(GC);
            string Result = HttpMethods.PostData(string.Format(UrlDefine.GetQunContactUrl, CommonDefine.LoginResult.pass_ticket), PostData, CommonDefine.Cookies, Encoding.UTF8).ContentData.ToString();
            if (!string.IsNullOrWhiteSpace(Result))
            {
                GroupMemberListEntity GroupMumberInfo = JsonConvert.DeserializeObject<GroupMemberListEntity>(Result);
                CommonDefine.GroupMemberList = GroupMumberInfo;
            }
            else
            {
                LogWriter.Write("获取群成员失败，返回结果为空!", LogPathDefine.WeChatGrouplogPath);
            }
            //GGC.List=new List<Entity.ListItem>(
        }

        #endregion

        #region 获取具体人信息

        /// <summary>
        /// 获取群成员昵称
        /// </summary>
        /// <param name="ali"></param>
        /// <returns></returns>
        public static GroupMemberEntity SelectGroupMumber(AddMsgListItemEntity ali)
        {
            try
            {
                string[] result = ali.Content.Split(':');

                var WhichGroupList = from item in CommonDefine.GroupMemberList.ContactList
                                     where item.UserName == ali.FromUserName
                                     select item;
                var WhichPersonList = from item in WhichGroupList.ToList().SingleOrDefault().MemberList
                                      where item.UserName == result[0]
                                      select item;
                return WhichPersonList.ToList<GroupMemberEntity>().SingleOrDefault();
            }
            catch (Exception ex)
            {
                LogWriter.Write("获取群成员名称失败！异常信息是:" + ex.Message, LogPathDefine.WeChatGrouplogPath);
                return null;
            }
        }

        #endregion

        #region 分析消息

        /// <summary>
        /// 分析消息结构
        /// </summary>
        /// <param name="MsgList"></param>
        private static void AnalyseMsg(List<AddMsgListItemEntity> MsgList)
        {
            try
            {
                //遍历所有消息项
                for (int index = 0; index < MsgList.Count; index++)
                {
                    AddMsgListItemEntity ali = MsgList[index];//消息体
                    MsgEntity me = new MsgEntity();//二次封装
                    me.IsCanAutoReply = true;
                    //  WebwxStatusNotify(ali.FromUserName, ali.ToUserName);
                    if (ali.FromUserName == CommonDefine.BaseContact.User.UserName) return;
                    //取得消息来源联系人信息
                    var CustomName = from item in CommonDefine.ContactsList.MemberList where item.UserName == ali.FromUserName select item;
                    if (CustomName == null || CustomName.ToList().Count <= 0)
                    {
                        //当前联系人列表中找不到此用户
                        try
                        {
                            //此处进行联系人刷新
                            RefreshContacts();
                            CustomName = from item in CommonDefine.ContactsList.MemberList where item.UserName == ali.FromUserName select item;
                            if (CustomName == null || CustomName.ToList().Count <= 0)//未找到消息来源
                                return;
                        }
                        catch (Exception ex)
                        {
                            LogWriter.Write("刷新联系人发生异常，异常信息是:" + ex.Message, LogPathDefine.ExceptionLogPath);
                        }
                    }
                    MemberListItemEntity Mlie = CustomName.ToList().SingleOrDefault();
                    try
                    {
                        if (Mlie.UserName.Contains("@@"))
                        {
                            //群消息处理
                            GroupMemberEntity Gme = SelectGroupMumber(ali);
                            if (Gme == null)
                            {
                                //刷新群成员
                                GetGroupContactsMethod(CommonDefine.ContactsList.MemberList);
                                Gme = SelectGroupMumber(ali);
                            }
                            if (ali.Content.Contains(":"))
                            {
                                string[] a = ali.Content.Split(':');
                                ali.Content = Gme.NickName + ":" + a[1].Replace("<br/>", "");
                            }
                            me.GroupMember = Gme;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogWriter.Write("替换群成员名称失败，异常信息为:" + ex.Message, LogPathDefine.ExceptionLogPath);
                    }
                    //消息列表实体
                    me.MsgOwer = Mlie;
                    me.MsgContent = ali.Content;
                    me.MsgOwerType = MsgOwerTypeEnum.AccepterMsg;
                    me.IsCanAutoReply = Mlie.IsCanAutoReply;

                    #region 储存消息

                    if (ali.MsgType == (int)MsgTypeEnum.Voice)
                    {
                        me.MsgType = MsgTypeEnum.Voice;
                        //语音消息
                        string Url = string.Format(UrlDefine.VoiceUrl, ali.MsgId, CommonDefine.LoginResult.skey);
                        string FileId = MethodsHelper.MsgSaveFile(DirectoryDefine.VoiceMsgPath);//获取FileId
                        //根据FileID创建本地的语音对象
                        List<byte> list = HttpMethods.GetFile(Url, Environment.CurrentDirectory + "\\" + DirectoryDefine.VoiceMsgPath + "\\" + FileId + ".mp3", CommonDefine.Cookies).ContentData as List<byte>;
                        me.MsgTime = DateTime.Now;
                        me.FileId = FileId;
                        me.FilePath = Environment.CurrentDirectory + "\\" + DirectoryDefine.VoiceMsgPath + "\\" + me.FileId + ".mp3";
                    }
                    else if (ali.MsgType == (int)MsgTypeEnum.Picture)
                    {
                        me.MsgType = MsgTypeEnum.Picture;
                        //图片消息
                        string Url = string.Format(UrlDefine.ImgUrlBig, ali.MsgId, CommonDefine.LoginResult.skey);
                        string FileId = MethodsHelper.MsgSaveFile(DirectoryDefine.ImageMsgPath);//获取FileId
                        //根据FileID创建本地的语音对象
                        List<byte> list = HttpMethods.GetFile(Url, Environment.CurrentDirectory + "\\" + DirectoryDefine.ImageMsgPath + "\\" + FileId + ".jpg", CommonDefine.Cookies).ContentData as List<byte>;
                        //获取缩略图
                        HttpMethods.GetFile(string.Format(UrlDefine.ImgUrl, ali.MsgId, CommonDefine.LoginResult.skey), Environment.CurrentDirectory + "\\" + DirectoryDefine.ImageMsgTempPath + "\\" + FileId + ".jpg", CommonDefine.Cookies);
                        //将下载下来的MP3文件转成Base64进行储存
                        //string Base64Str = Convert.ToBase64String(list.ToArray());
                        me.MsgTime = DateTime.Now;
                        me.FileId = FileId;
                        me.FilePath = Environment.CurrentDirectory + "\\" + DirectoryDefine.ImageMsgPath + "\\" + FileId + ".jpg";//
                        me.FileTempPath = Environment.CurrentDirectory + "\\" + DirectoryDefine.ImageMsgTempPath + "\\" + FileId + ".jpg";
                    }
                    else if (ali.MsgType == (int)MsgTypeEnum.Gif)
                    {
                        //TODO:GIF消息
                        me.MsgType = MsgTypeEnum.Gif;
                        //语音消息
                        string Url = string.Format(UrlDefine.ImgUrlBig, ali.MsgId, CommonDefine.LoginResult.skey);
                        string FileId = MethodsHelper.MsgSaveFile(DirectoryDefine.ImageMsgGifPath);//获取FileId
                        HttpMethods.GetFile(Url, Environment.CurrentDirectory + "\\" + DirectoryDefine.ImageMsgGifPath + "\\" + FileId + ".gif", CommonDefine.Cookies);
                        HttpMethods.GetFile(string.Format(UrlDefine.ImgUrl, ali.MsgId, CommonDefine.LoginResult.skey), Environment.CurrentDirectory + "\\" + DirectoryDefine.ImageMsgTempPath + "\\" + FileId + ".jpg", CommonDefine.Cookies);
                        me.MsgTime = DateTime.Now;
                        me.FileId = FileId;
                        me.FilePath = Environment.CurrentDirectory + "\\" + DirectoryDefine.ImageMsgGifPath + "\\" + FileId + ".gif";//
                        me.FileTempPath = Environment.CurrentDirectory + "\\" + DirectoryDefine.ImageMsgTempPath + "\\" + FileId + ".jpg";
                    }
                    else if (ali.MsgType == (int)MsgTypeEnum.Video)
                    {
                        me.MsgType = MsgTypeEnum.Video;
                        //视频消息
                        string Url = string.Format(UrlDefine.VideoUrl, ali.MsgId, HttpUtility.UrlEncode(CommonDefine.LoginResult.skey));
                        string FileId = MethodsHelper.MsgSaveFile(DirectoryDefine.VideoMsgPath);//获取FileId
                        HttpMethods.GetFile(string.Format(UrlDefine.ImgUrl, ali.MsgId, CommonDefine.LoginResult.skey), Environment.CurrentDirectory + "\\" + DirectoryDefine.ImageMsgTempPath + "\\" + FileId + ".jpg", CommonDefine.Cookies);
                        //根据FileID创建本地的语音对象
                        List<byte> list = HttpMethods.GetVideo(Url, Environment.CurrentDirectory + "\\" + DirectoryDefine.VideoMsgPath + "\\" + FileId + ".mp4", CommonDefine.Cookies);
                        me.MsgTime = DateTime.Now;
                        me.FileId = FileId;
                        me.FilePath = Environment.CurrentDirectory + "\\" + DirectoryDefine.VideoMsgPath + "\\" + FileId + ".mp4";//
                        me.FileTempPath = Environment.CurrentDirectory + "\\" + DirectoryDefine.ImageMsgTempPath + "\\" + FileId + ".jpg";
                    }
                    else if (ali.MsgType == 1 && !string.IsNullOrWhiteSpace(ali.Url))
                    {
                        //地图消息
                        me.MsgType = MsgTypeEnum.Map;
                        me.MsgUrl = ali.Url;
                        //地图消息
                        string Url = string.Format(UrlDefine.MapUrl, ali.MsgId);
                        //HttpHelper.ContentType = "";
                        string FileId = MethodsHelper.MsgSaveFile(DirectoryDefine.MapImageMsgPath);//获取FileId
                        //根据FileID创建本地的语音对象
                        HttpMethods.GetFile(Url, Environment.CurrentDirectory + "\\" + DirectoryDefine.MapImageMsgPath + "\\" + FileId + ".jpg", CommonDefine.Cookies);
                        me.FilePath = Environment.CurrentDirectory + "\\" + DirectoryDefine.MapImageMsgPath + "\\" + FileId + ".jpg";
                        me.MsgTime = DateTime.Now;
                    }
                    else if (ali.MsgType == (int)MsgTypeEnum.SystemMsg)
                    {
                        me.MsgType = MsgTypeEnum.SystemMsg;
                        me.MsgTime = DateTime.Now;
                    }
                    else
                    {
                        me.MsgType = MsgTypeEnum.Text;
                        me.MsgTime = DateTime.Now;
                    }
                    LogWriter.Write(me.MsgContent.ToString(), LogPathDefine.WeChatLogPath);
                    CommonMethodCallBackHandlers.OnReceivedMsgAnalyseMsgCompleted(me);
                    SetAutoRepate(CommonDefine.WhiteUserList, LoadAutoReplyConfig(ConfigDefine.WeChatAutoReplyPath), me.MsgOwer.UserName, me);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogWriter.Write(ex.Message, LogPathDefine.ExceptionLogPath);
            }

        }

        #endregion

        #region 发送消息

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="Msg">消息体</param>
        /// <param name="IsAddChatList">是否添加到聊天列表</param>
        /// <param name="ToName">向谁发送</param>
        public static bool SendToOtherMsg(string Msg = "", string ToName = "")
        {
            string SendMsg = string.Empty;
            string LocalMsgId = MethodsHelper.GetClientMsgId();
            if (!string.IsNullOrWhiteSpace(Msg))
                SendMsg = Msg;
            string chaturl = UrlDefine.ChatUrl + CommonDefine.LoginResult.pass_ticket;
            SendMsgBodyEntity Smbe = new SendMsgBodyEntity() { BaseRequest = new BaseRequestEntity() { DeviceID = MethodsHelper.GetDeviceId(), Uin = CommonDefine.LoginResult.wxuin, Sid = CommonDefine.LoginResult.wxsid, Skey = CommonDefine.LoginResult.skey }, Msg = new SendMsgEntity() { Type = MsgTypeEnum.Text, FromUserName = CommonDefine.BaseContact.User.UserName, ToUserName = ToName, Content = Msg, LocalID = LocalMsgId, ClientMsgId = LocalMsgId } };
            string PostData = JsonConvert.SerializeObject(Smbe);
            string Result = HttpMethods.PostData(chaturl, PostData, CommonDefine.Cookies, Encoding.UTF8, "AcceptEncoding", "gzip, deflate").ContentData.ToString();
            SendMsgResultEntity Sre = JsonConvert.DeserializeObject<SendMsgResultEntity>(Result);
            if (Sre.BaseResponse.Ret == 0)
            {
                //消息发送成功
                CommonMethodCallBackHandlers.OnSendMsgCompleted(true);
                return true;
            }
            else
            {
                Console.WriteLine("发送失败:" + Result);
                CommonMethodCallBackHandlers.OnSendMsgCompleted(false);
                return false;
            }
        }

        #endregion

        #region 同步消息

        /// <summary>
        /// 对手机端信息进行同步
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        public static void WebwxStatusNotify(string FromUserName, string ToUserName)
        {
            NotifyPhoneEntity Npe = new NotifyPhoneEntity();
            BaseRequestEntity br = new BaseRequestEntity();
            br.DeviceID = MethodsHelper.GetDeviceId();
            br.Sid = CommonDefine.LoginResult.wxsid;
            br.Skey = CommonDefine.LoginResult.skey;
            br.Uin = CommonDefine.LoginResult.wxuin;
            Npe.BaseRequest = br;
            Random ran = new Random();
            Npe.ClientMsgId = MethodsHelper.GetClientMsgId();// "1497590764" + ((int)ran.Next(0, 9)).ToString() + ((int)ran.Next(0, 9)).ToString() + ((int)ran.Next(0, 9)).ToString();
            Npe.Code = "1";
            Npe.FromUserName = FromUserName;
            Npe.ToUserName = ToUserName;
            string PostData = JsonConvert.SerializeObject(Npe);
            string list = HttpMethods.PostData(UrlDefine.WexNotifyUrl + CommonDefine.LoginResult.pass_ticket, PostData, CommonDefine.Cookies, Encoding.UTF8).ContentData.ToString();
            if (list != null)
            {
                SendMsgResultEntity Sre = JsonConvert.DeserializeObject<SendMsgResultEntity>(list);
                if (Sre.BaseResponse.Ret == 0)
                {
                }
                //if (list.Count >= 1)
                //{
                //    Console.WriteLine(list[0].ToString());
                //}
            }

        }

        #endregion

        #region 自动回复
        /// <summary>
        /// 设置自动回复
        /// </summary>
        /// <param name="UserList">白名单</param>
        /// <param name="AutoRelyList">自动回复列表</param>
        /// <param name="UserName">用户名</param>
        /// <param name="MsgContent">消息内容（不用管)</param>
        internal static void SetAutoRepate(List<MemberListItemEntity> UserList, List<AutoRepateEntity> AutoRelyList, string UserName, MsgEntity MsgContent)
        {
            if (UserList == null) UserList = new List<MemberListItemEntity>();
            if (AutoRelyList.Count == 0)
            {
                LogWriter.Write("AutoReply为空!", LogPathDefine.WeChatLogPath);
                return;
            }
            try
            {
                string ReplyBody = "{0}{1},回复{2}关闭自动回复，回复{3}开启自动回复";
                string[] GroupNumber = new string[2];
                if (MsgContent.MsgContent.ToString().Contains(":"))
                {
                    GroupNumber = MsgContent.MsgContent.ToString().Split(':');
                }
                var data = from item in UserList where item.UserName == UserName select item;
                if (data != null && data.ToList().Count > 0)
                {
                    //说明当前用户在白名单中，所以不能设置自动回复
                }
                else
                {
                    var CommnonReplyData = from item in AutoRelyList where item.AutoRepateType == AutoRepateEnum.CommnonReply select item;
                    var ThanksReplyData = from item in AutoRelyList where item.AutoRepateType == AutoRepateEnum.ThanksReply select item;
                    AutoRepateEntity CommnonReplyDataEntity = CommnonReplyData.SingleOrDefault();
                    AutoRepateEntity ThanksReplyDataEntity = ThanksReplyData.SingleOrDefault();
                    if (MsgContent.MsgContent.ToString() == CommnonReplyDataEntity.AutoReplySwitch)
                    {
                        //关闭自动回复
                        CommonDefine.ContactsList.MemberList.ForEach(p =>
                        {
                            if (p.UserName == UserName)
                            {
                                p.IsCanAutoReply = false;
                                MsgContent.IsCanAutoReply = false;
                            }
                        });
                        SendToOtherMsg(ThanksReplyDataEntity.ReplyContent, UserName);
                    }
                    if (MsgContent.MsgContent.ToString() == ThanksReplyDataEntity.AutoReplySwitch)
                    {
                        CommonDefine.ContactsList.MemberList.ForEach(p =>
                        {
                            if (p.UserName == UserName)
                            {
                                p.IsCanAutoReply = true;
                                MsgContent.IsCanAutoReply = true;
                            }
                        });
                    }
                    if (MsgContent.IsCanAutoReply)
                    {
                        string GroupName = string.Empty;
                        if (GroupNumber != null && GroupNumber.Length > 0)
                            GroupName = GroupNumber[0];
                        SendToOtherMsg(string.Format(ReplyBody, UserName.Contains("@@") ? "" : GroupName, CommnonReplyDataEntity.ReplyContent, CommnonReplyDataEntity.AutoReplySwitch, ThanksReplyDataEntity.AutoReplySwitch), UserName);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.Write(ex.Message, LogPathDefine.ExceptionLogPath);
            }
        }

        #endregion

        #region 发送文件
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="Fe">文件类型</param>
        /// <param name="FileData">文件byte</param>

        public static void SendFile(FileEntity Fe, byte[] FileData)
        {
            Fe.BaseRequest = new BaseRequestEntity() { DeviceID = MethodsHelper.GetDeviceId(), Uin = CommonDefine.LoginResult.wxuin, Sid = CommonDefine.LoginResult.wxsid, Skey = CommonDefine.LoginResult.skey };
            string Boundary = "------WebKitFormBoundaryUOlSZtOip6JP4NSr";
            HttpMethods.AddContent("id", "WU_FILE_0", Boundary);
            HttpMethods.AddContent("name", Fe.FileName, Boundary);
            HttpMethods.AddContent("type", Fe.FileType, Boundary);
            HttpMethods.AddContent("lastModifiedDate", Fe.FileModiftTime.ToLocalTime().ToString(), Boundary);
            HttpMethods.AddContent("size", Fe.DataLen.ToString(), Boundary);
            HttpMethods.AddContent("mediatype", Fe.Mediatype1, Boundary);
            HttpMethods.AddContent("uploadmediarequest", JsonConvert.SerializeObject(Fe), Boundary);
            HttpMethods.AddContent("webwx_data_ticket", CommonDefine.GetCookieDictionary == null ? "" : CommonDefine.GetCookieDictionary["webwx_data_ticket"], Boundary);//"gScOa2YvEEtszEvhoxdwXCeg"
            HttpMethods.AddContent("pass_ticket", CommonDefine.LoginResult.pass_ticket, Boundary);//"ekYYf2P7xOH5iI+oYrf/GiGrZhE+fUEGMiXe3Pq71rb4MDe+7ICbD+kglD4ZE+Ey"
            HttpMethods.AddContent("filename", Fe.FileName, Fe.FileType, FileData, Boundary);
            HttpMethods.OptionsDataMulitData(UrlDefine.SendFileUrl, CommonDefine.Cookies, Encoding.UTF8, Boundary);
            HttpMethods.PostDataMulitData(UrlDefine.SendFileUrl, CommonDefine.Cookies, Encoding.UTF8, Boundary);
        }
        #endregion

        #region 加载自动回复配置文件
        /// <summary>
        /// 读取自动回复配置文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static List<AutoRepateEntity> LoadAutoReplyConfig(string path)
        {
            try
            {
                List<AutoRepateEntity> ResultList = new List<AutoRepateEntity>();
                if (!File.Exists(path))
                    return ResultList;
                ResultList = JsonConvert.DeserializeObject<List<AutoRepateEntity>>(File.ReadAllText(path));
                if (ResultList == null)
                    return new List<AutoRepateEntity>();
                else
                    return ResultList;
            }
            catch (Exception ex)
            {
                LogWriter.Write(ex.Message, LogPathDefine.ExceptionLogPath);
                return new List<AutoRepateEntity>();
            }
        }
        #endregion
    }
}

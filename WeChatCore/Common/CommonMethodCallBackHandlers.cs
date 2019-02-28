using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatCore.Entity;

namespace WeChatCore.Common
{
    public static class CommonMethodCallBackHandlers
    {
        /// <summary>
        /// 下载二维码完成
        /// </summary>
        public static event Action DownLoadQRCodeCompleted;
        /// <summary>
        /// 下载二维码完成
        /// </summary>
        public static void OnDownLoadQRCodeCompleted()
        {
            if (DownLoadQRCodeCompleted != null)
                DownLoadQRCodeCompleted.Invoke();
            else
                throw new Exception("未实现方法CommonMethodCallBackHandlers.DownLoadQRCodeCompleted");
        }
        public static event Action<string> LoginScranQRCodeCompleted;
        /// <summary>
        /// s扫描二维码等待登录
        /// </summary>
        /// <param name="e"></param>
        public static void OnLoginScranQRCodeCompleted(string Path)
        {
            if (LoginScranQRCodeCompleted != null)
                LoginScranQRCodeCompleted.Invoke(Path);
            else
                throw new Exception("未实现方法CommonMethodCallBackHandlers.LoginScranQRCodeCompleted");
        }
        /// <summary>
        /// 登录完成
        /// </summary>
        public static event Action<bool> LoginCompleted;
        /// <summary>
        /// 登录成功
        /// </summary>
        public static void OnLoginCompleted(bool e = false)
        {

            if (LoginCompleted != null)
                LoginCompleted.Invoke(e);
            else
                throw new Exception("未实现方法CommonMethodCallBackHandlers.LoginCompleted");
        }
        public static event Action<MsgEntity> ReceivedMsgAnalyseMsgCompleted;
        /// <summary>
        /// 接受并分析消息
        /// </summary>
        /// <param name="Me">重新封装组合的消息列表</param>
        public static void OnReceivedMsgAnalyseMsgCompleted(MsgEntity Me)
        {
            if (ReceivedMsgAnalyseMsgCompleted != null)
                ReceivedMsgAnalyseMsgCompleted.Invoke(Me);
            else
                throw new Exception("未实现方法CommonMethodCallBackHandlers.ReceivedMsgAnalyseMsgCompleted");
        }
        /// <summary>
        /// 发送消息完成
        /// </summary>
        public static event Action<bool> SendMsgCompleted;
        /// <summary>
        /// 发送消息完成
        /// </summary>
        /// <param name="e"></param>
        public static void OnSendMsgCompleted(bool e)
        {
            if (SendMsgCompleted != null)
                SendMsgCompleted.Invoke(e);
            //else
            //    throw new .Exception("未实现方法CommonMethodCallBackHandlers.SendMsgCompleted");
        }
    }
}

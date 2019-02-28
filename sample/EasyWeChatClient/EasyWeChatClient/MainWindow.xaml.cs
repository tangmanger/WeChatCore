using EasyWeChatClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeChatCore;
using WeChatCore.Common;
using WeChatCore.Entity;

namespace EasyWeChatClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.SetOut(new TextBoxWriter(a1));
            CommonMethodCallBackHandlers.DownLoadQRCodeCompleted += CommonMethodCallBackHandlers_DownLoadQRCodeCompleted;
            CommonMethodCallBackHandlers.LoginScranQRCodeCompleted += CommonMethodCallBackHandlers_LoginScranQRCodeCompleted;
            CommonMethodCallBackHandlers.LoginCompleted += CommonMethodCallBackHandlers_LoginCompleted;
            CommonMethodCallBackHandlers.ReceivedMsgAnalyseMsgCompleted += CommonMethodCallBackHandlers_ReceivedMsgAnalyseMsgCompleted;
            CommonMethodCallBackHandlers.SendMsgCompleted += CommonMethodCallBackHandlers_SendMsgCompleted;
            new Task(() =>
            {
                WeChat.InitWeChatRobot();
            }).Start();
        }

        void CommonMethodCallBackHandlers_SendMsgCompleted(bool obj)
        {
            
        }

        void CommonMethodCallBackHandlers_ReceivedMsgAnalyseMsgCompleted(MsgEntity obj)
        {
            
        }

        private void CommonMethodCallBackHandlers_LoginCompleted(bool obj)
        {
            if (obj)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Image1.Visibility = Visibility.Collapsed;
                    a1.Visibility = Visibility.Visible;
                }));
            }
        }

        private void CommonMethodCallBackHandlers_LoginScranQRCodeCompleted(string obj)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                Image1.Source = new BitmapImage(new Uri(obj));
            }
            ));
        }

        private void CommonMethodCallBackHandlers_DownLoadQRCodeCompleted()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                Image1.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Check.jpg"));
            }));
        }

        private void a1_TextChanged(object sender, TextChangedEventArgs e)
        {
            a1.ScrollToEnd();
        }
    }
}

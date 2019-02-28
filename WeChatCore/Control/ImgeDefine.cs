using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WeChatCore.DefineClass;

namespace WeChatCore.Control
{
    public class ImgeDefine : Image
    {
        private static readonly DependencyProperty ImageSourceDefineProperty = DependencyProperty.Register("ImageSourceDefine", typeof(string), typeof(ImgeDefine), new PropertyMetadata(new PropertyChangedCallback(ImageSourceCallBack)));

        private static void ImageSourceCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImgeDefine id = d as ImgeDefine;
            //ToDo
            new Task(() =>
            {
                List<byte> b = (List<byte>)HttpHelper.HttpMethods.GetFile(UrlDefine.RootUrl + id.ImageSourceDefine, "", CommonDefine.Cookies).ContentData;
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    MemoryStream ms = new MemoryStream(b.ToArray());
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = ms;// new FileStream();
                    bi.EndInit();
                    id.Source = bi;
                }));
            }).Start();
            //if (string.IsNullOrWhiteSpace(HeadUrlDef))
            //{
            //    // string uuid = MethodsHelper.MsgSaveFile(DirectoryDefine.HeaderImagePath);
            //    HttpHelper.HttpMethods.GetFile(UrlDefine.RootUrl + HeadImgUrl, Environment.CurrentDirectory + "\\" + DirectoryDefine.HeaderImagePath + "\\" + uuid + ".jpg", CommonDefine.Cookies);
            //    Application.Current.Dispatcher.Invoke(new Action(() =>
            //    {
            //        id.Source = Environment.CurrentDirectory + "\\" + DirectoryDefine.HeaderImagePath + "\\" + uuid + ".jpg";
            //    }));
            //}
            //else
            //{
            //    LogWriter.Write(string.Format("当前人员{0}已有头像", DisplayNameDef), LogPathDefine.WeChatLogPath);
            //}
        }

        public string ImageSourceDefine
        {
            get
            {
                return (string)GetValue(ImageSourceDefineProperty);
            }
            set
            {
                SetValue(ImageSourceDefineProperty, value);
            }
        }
    }
}

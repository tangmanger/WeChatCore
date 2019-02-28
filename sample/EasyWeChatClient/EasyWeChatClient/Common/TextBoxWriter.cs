using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EasyWeChatClient.Common
{
    public class TextBoxWriter : TextWriter
    {
        TextBox textBox;
        delegate void WriteFunc(string value);
        WriteFunc write;
        WriteFunc writeLine;

        public TextBoxWriter(TextBox textBox)
        {
            this.textBox = textBox;
            write = Write;
            writeLine = WriteLine;
        }


        // 使用UTF-16避免不必要的编码转换
        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }


        // 最低限度需要重写的方法
        public override void Write(string value)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                textBox.AppendText(value);
            }));
        }


        // 为提高效率直接处理一行的输出
        public override void WriteLine(string value)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                textBox.AppendText(value);
                textBox.AppendText(this.NewLine);
            }));
        }
    }
}

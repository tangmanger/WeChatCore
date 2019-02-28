using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WeChatCore.Common
{

    public class NotifyPropertyChanged : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>

        /// 一般实现通知办法

        /// </summary>

        /// <param name="PropertyName"></param>

        public void RaisePropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        /// <summary>

        /// 监听属性改变并发出通知

        /// </summary>

        /// <typeparam name="T">类型</typeparam>

        /// <param name="proertyExpression">属性表达式</param>

        public void RaisePropertyChanged<T>(Expression<Func<T>> proertyExpression)
        {
            if (PropertyChanged != null)
            {
                string propertyName = GetPropertyName<T>(proertyExpression);
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// </summary>

        /// <typeparam name="T">属性</typeparam>

        /// <param name="proertyExpression">Linq表达式</param>

        /// <returns></returns>

        private string GetPropertyName<T>(Expression<Func<T>> proertyExpression)
        {
            if (proertyExpression == null)
            {
                throw new ArgumentNullException("proertyExpression");
            }
            MemberExpression mp = proertyExpression.Body as MemberExpression;
            if (mp == null)
            {
                throw new ArgumentException("Invalid Argument", "proertyExpression");
            }
            PropertyInfo proInfo = mp.Member as PropertyInfo;
            if (proInfo == null)
            {
                throw new ArgumentException("Argument is not a property", "proertyExpression");
            }
            return proInfo.Name;
        }
    }
}

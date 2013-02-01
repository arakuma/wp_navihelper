using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Text;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using System.Linq;

namespace Arakuma.Ui.Utils {
    /// <summary>
    /// 页面切换工具类
    /// </summary>
    public static class NavigationHelper {

        /// <summary>
        /// 切换页面
        /// </summary>
        /// <param name="pageName">页面地址</param>
        /// <param name="parameters">参数列表</param>
        public static void Go( string pageName, params KeyValuePair<string,string>[] parameters ) {            
            string fullUri = pageName;
            if ( parameters != null && parameters.Length > 0 ) {
                // /views/view.xaml?
                StringBuilder sb = new StringBuilder( fullUri += "?" );
                // /views/view.xaml?k=v&
                foreach ( KeyValuePair<string,string> parameter in parameters ) {
                    sb.Append( parameter.Key );
                    sb.Append( "=" );
                    sb.Append( parameter.Value );
                    sb.Append( "&" );
                }
                // /views/view.xaml?k=v
                fullUri = sb.ToString().TrimEnd( '&' );
            }                        
            ( Application.Current.RootVisual as PhoneApplicationFrame ).Navigate( new Uri( fullUri, UriKind.Relative ) );
        }

        /// <summary>
        /// 跳转页面，传递数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageName"></param>
        /// <param name="data"></param>
        /// <param name="parameters"></param>
        public static void Go<T>( string pageName, T data, params KeyValuePair<string, string>[] parameters ) {
            PageDataHandler<T> dataHandler = new FilePageDataHandler<T>();
            Go( pageName, dataHandler.SaveData( data, parameters ) );
        }

        /// <summary>
        /// 从参数中提取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        public static T ExtractData<T>( Dictionary<string, string> queryStrings ) {
            PageDataHandler<T> dataHandler = new FilePageDataHandler<T>();
            return dataHandler.LoadData( queryStrings );
        }

        /// <summary>
        /// 页面返回
        /// </summary>
        public static void GoBack() {
            var frame = Application.Current.RootVisual as PhoneApplicationFrame;
            if ( frame.CanGoBack ) {
                frame.GoBack();
            }
        }

        /// 页面前进
        /// </summary>
        public static void GoForward() {
            var frame = Application.Current.RootVisual as PhoneApplicationFrame;
            if ( frame.CanGoForward ) {
                frame.GoForward();
            }
        }

        /// <summary>
        /// 清空BackStack
        /// </summary>
        public static void ClearBackStack() {
            var frame = Application.Current.RootVisual as PhoneApplicationFrame;
            while ( frame.BackStack.Count()>0 ) 
                frame.RemoveBackEntry();            
        }

        /// <summary>
        /// 获取BackStack
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<JournalEntry> GetBackStack() {
            var frame = Application.Current.RootVisual as PhoneApplicationFrame;
            return frame.BackStack;            
        }
    }
}

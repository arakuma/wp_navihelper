using System.Collections.Generic;

namespace Arakuma.Ui.Utils {

    /// <summary>
    /// 数据处理基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    abstract class PageDataHandler<T> {
        private static readonly string PAGEKEY_DATA = "pagedata";

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">文件名</param>
        public abstract KeyValuePair<string, string>[] SaveData( T data, KeyValuePair<string, string>[] parameters );
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public abstract T LoadData( Dictionary<string, string> queryStrings );
        /// <summary>
        /// 增加新的data参数
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected KeyValuePair<string, string>[] AddDataParameter( string value, KeyValuePair<string, string>[] parameters ) {
            KeyValuePair<string,string>[] newParams = null;
            if ( parameters == null ) {
                newParams = new KeyValuePair<string, string>[1];
            }
            else {
                newParams = new KeyValuePair<string, string>[parameters.Length + 1];
                for ( int i = 0; i < parameters.Length; i++ ) {
                    newParams[i + 1] = parameters[i];
                }
            }
            newParams[0] = new KeyValuePair<string, string>( PAGEKEY_DATA, value );
            return newParams;
        }
        /// <summary>
        /// 提取url中data field的value
        /// </summary>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        protected string ExtractDataValue( Dictionary<string, string> queryStrings ) {
            if ( queryStrings.ContainsKey( PAGEKEY_DATA ) ) {
                return queryStrings[PAGEKEY_DATA];
            }
            return string.Empty;
        }
    }


}

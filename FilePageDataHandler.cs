using Arakuma.Data;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace Arakuma.Ui.Utils {

    /// <summary>
    /// 利用文件传递数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class FilePageDataHandler<T> : PageDataHandler<T> {
        /// <summary>
        /// 临时文件名
        /// </summary>
        private static readonly string TEMP_FILE_FORMAT = "{0}.pgd";

        /// <summary>
        /// 保存数据到硬盘
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override KeyValuePair<string, string>[] SaveData( T data, KeyValuePair<string, string>[] parameters ) {
            JsonSerializationHelper<T> helper = new JsonSerializationHelper<T>();
            var dataFileName = string.Format( TEMP_FILE_FORMAT, DateTime.Now.Ticks );
            helper.Serialize( dataFileName, data );
            return AddDataParameter( dataFileName, parameters );
        }

        /// <summary>
        /// 提取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        public override T LoadData( Dictionary<string, string> queryStrings ) {
            T data = default( T );
            var dataFile = ExtractDataValue( queryStrings );
            if ( !string.IsNullOrEmpty( dataFile ) ) {
                JsonSerializationHelper<T> helper = new JsonSerializationHelper<T>();
                helper.Deserialize( dataFile, ref data );

                using ( var storage = IsolatedStorageFile.GetUserStoreForApplication() ) {
                    try {
                        storage.DeleteFile( dataFile );
                    }
                    catch ( Exception ) { }
                }
            }
            return data;
        }
    }
}

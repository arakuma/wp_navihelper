using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakuma.Ui.Utils {
    /// <summary>
    /// 利用url传递参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class UrlPageDataHandler<T> : PageDataHandler<T> {
        /// <summary>
        /// 注意方法内并没有做urlencode
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override KeyValuePair<string, string>[] SaveData( T data, KeyValuePair<string, string>[] parameters ) {
            string content = JsonConvert.SerializeObject( data );
            return AddDataParameter( content, parameters );
        }

        /// <summary>
        /// 提取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        public override T LoadData( Dictionary<string, string> queryStrings ) {
            string content = ExtractDataValue( queryStrings );
            return JsonConvert.DeserializeObject<T>( content );
        }
    }
}

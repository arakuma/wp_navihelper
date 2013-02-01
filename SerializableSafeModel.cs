using Arakuma.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationHelper {
    /// <summary>
    /// 可安全序列化的对象基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    abstract class SerializableSafeModel<T> {
        public SerializableDictionary<string, string> Dict;
        public T Source;
        public SerializableSafeModel( T originalSource, IDictionary<string, string> dict ) {
            Source = originalSource;
            Dict = new SerializableDictionary<string, string>();
            if ( dict != null ) {
                foreach ( var kvp in dict ) {
                    Dict.Add( kvp.Key, kvp.Value );
                }
            }
        }
        public abstract T Unwrap();
    }
}

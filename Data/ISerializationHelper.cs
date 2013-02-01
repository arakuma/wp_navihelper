
namespace Arakuma.Data {
    /// <summary>
    /// Interface for serialization
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface ISerializationHelper<T> {
        void Serialize( string fileName, T data );
        void Deserialize( string fileName, ref T data );
    }
}

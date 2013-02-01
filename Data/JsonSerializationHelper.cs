using System;
using System.IO;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;

namespace Arakuma.Data {
    /// <summary>
    /// Json serialization implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonSerializationHelper<T> : ISerializationHelper<T> {

        /// <summary>
        /// Serialize data to data file
        /// </summary>
        /// <param name="fileName">data file name</param>
        /// <param name="data">data object</param>
        public void Serialize( string fileName, T data ) {
            using ( IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication() ) {

                IsolatedStorageFileStream fileStream = storage.CreateFile( fileName );
                try {
                    string content = JsonConvert.SerializeObject( data );
                    if ( fileStream != null && data != null ) {
                        StreamWriter writer = new StreamWriter( fileStream );
                        writer.Write( content );
                        writer.Flush();
                    }
                }
                catch ( InvalidOperationException ) { }
                catch ( Exception ) { }
                finally {
                    if ( fileStream != null ) {
                        fileStream.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Deserialize data
        /// </summary>
        /// <param name="fileName">data file name</param>
        /// <param name="data">reference data, result could be null for reference type</param>
        public void Deserialize( string fileName, ref T data ) {
            using ( IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication() ) {
                if ( !storage.FileExists( fileName ) )
                    // no bothering to deserialization when file is not there
                    // often this is the first time application launching
                    return;

                IsolatedStorageFileStream fileStream = storage.OpenFile( fileName, System.IO.FileMode.OpenOrCreate );
                try {
                    StreamReader reader = new StreamReader( fileStream );
                    T deserializedData = JsonConvert.DeserializeObject<T>( reader.ReadToEnd() );
                    if ( deserializedData != null && deserializedData is T ) {
                        data = deserializedData;
                    }
                    else {
                        data = default( T );
                    }
                }
                catch ( InvalidOperationException e ) {}
                catch ( Exception e ) {}
                finally {
                    if ( fileStream != null ) {
                        fileStream.Close();
                    }
                }
            }
        }
    }
}

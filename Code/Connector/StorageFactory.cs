using Helpers;

namespace Connectors
{
    
    public class StorageFactory
    {

        /// <summary> 
        /// Create a FilesFactory
        /// </summary>
        /// <returns>A new Instance for the logger</returns>
        public static IStorageFactory CreateFileConnetor()
        {
            IStorageFactory file;
            if (ConfigurationHelper.EnableAmazoneS3)
            {
                file = new AmazoneS3Connector();
            }
            else
            {
                file = new LocalStorageConnector();
            }
            return file;

        }
    }
}

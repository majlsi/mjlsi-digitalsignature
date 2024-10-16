using Amazon.S3.Model;
using Connectors;
using Helpers;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Services
{
  public class FileService : IFileService
  {
       
        public async Task<string> Upload(IFormFile file, string WebRootPath, string folderName, string fileName)
        {
            string returnPath = null;
             var fileConnector = StorageFactory.CreateFileConnetor();
            returnPath = await fileConnector.UploadFile(file , WebRootPath, folderName, fileName);    
            return returnPath;
        }

        public async Task<ListVersionsResponse> FilesList()
        {
            IStorageFactory fileConnector = StorageFactory.CreateFileConnetor();
            var result = await fileConnector.FilesList();
            
            return result;
        }
        public async Task<Stream> GetFile(string key)
        {
            key = key.Replace("/", "\\");
            IStorageFactory fileConnector = StorageFactory.CreateFileConnetor();
            var result = await fileConnector.GetFile( "",key);
            return result;
        }
    }
}

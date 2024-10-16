using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors
{
    public interface IStorageFactory
    {
        Task<string> UploadFile(IFormFile file, string WebRootPath, string folderName, string fileName);
       
        Task<bool> DeleteFile(string key);
        Task<int> DirectoryFilesAsync(string Prefix , string fullPath);
        Task<ListVersionsResponse> FilesList();
        Task<bool> CheckFileExists(string webRootPath, string OriginalDocumentUrl);
        Task<Stream> GetFile(string webRootPath, string key);
      
    }
}

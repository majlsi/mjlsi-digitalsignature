using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
   public interface IFileService
    {
        Task<string> Upload(IFormFile file, string WebRootPath, string folderName, string fileName);

        Task<ListVersionsResponse> FilesList();
        Task<Stream> GetFile(string key);

    }
}

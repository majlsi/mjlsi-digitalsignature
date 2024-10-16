using Amazon.S3.Model;
using Helpers;
using Microsoft.AspNetCore.Http;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Connectors
{
    public class LocalStorageConnector : IStorageFactory
    {
        public async Task<bool> CheckFileExists(string webRootPath, string OriginalDocumentUrl)
        {
            string pdfFilePath = Path.Combine(webRootPath, OriginalDocumentUrl);
            FileInfo f = new FileInfo(pdfFilePath);
            if (!f.Exists)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteFile(string key)
        {
            string[] files = Directory.GetFiles(key);
            foreach (string file in files)
            {
                File.Delete(file);
              
            }
            return  true;
        }

      
        public async Task<int> DirectoryFilesAsync(string location , string fullPath)
        {
            int PagesCount = Directory.GetFiles(fullPath, "*.*", SearchOption.AllDirectories).Length;
            return PagesCount;
           
        }

        public Task<ListVersionsResponse> FilesList()
        {
            return null;
        }

        public async Task<Stream> GetFile(string webRootPath, string key)
        {

            string path = Path.Combine(webRootPath, key);
            var stream = new FileStream(path, FileMode.Open);
            return stream;
        }


        public async Task<string> UploadFile(IFormFile file, string WebRootPath, string folderName, string fileName)
        {
            string newPath = Path.Combine(WebRootPath, folderName);
            string returnPath = string.Empty;
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                // fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string fullPath = Path.Combine(newPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                returnPath = $"{folderName}{Path.DirectorySeparatorChar}{fileName}";
            }
            return returnPath;

        }
    }
}

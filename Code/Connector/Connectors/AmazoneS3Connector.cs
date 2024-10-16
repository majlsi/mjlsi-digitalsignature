using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Helpers;
using Loggers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Connectors
{
    public class AmazoneS3Connector : IStorageFactory
    {
        public async Task<string> UploadFile(IFormFile file, string WebRootPath, string folderName, string fileName)
        {
            string returnPath = null;
            try
            {
               
                if (file.Length > 0)
                {

                    string fullPath = Path.Combine(folderName, fileName);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileConnector = StorageFactory.CreateFileConnetor();
                        bool result = await UploadToAmazom(ms, fullPath);
                        if (result)
                        {

                            returnPath = fullPath.Replace(Path.DirectorySeparatorChar.ToString(), $"{Path.DirectorySeparatorChar}{Path.DirectorySeparatorChar}");

                        }
                    }
                }
               

            }
            catch (AmazonS3Exception ex)
            {

              
                string loggerFile =  "AmazoneS3/Excpetion";
                ILogger logger = LoggerFactory.CreateLogger( loggerFile);
                logger.Error(ex);
            }
            catch (Exception ex)
            {
              
                string loggerFile =  "AmazoneS3/Excpetion";
                ILogger logger = LoggerFactory.CreateLogger( loggerFile);
                logger.Error(ex);
            }
            return returnPath;
        }
        public async Task<ListVersionsResponse> FilesList()
        {
            AmazonS3Client _amazonS3 = new AmazonS3Client(ConfigurationHelper.AmazoneAccessKey, ConfigurationHelper.AmazoneSecretAccessKey, new AmazonS3Config() { ServiceURL = ConfigurationHelper.AmazoneURL });
            ListVersionsResponse listVersions = await _amazonS3.ListVersionsAsync(ConfigurationHelper.AmazoneBucket);
            string loggerFile = "AmazoneS3/Response";
            ILogger logger = LoggerFactory.CreateLogger( loggerFile);
            string responseString = JsonConvert.SerializeObject(listVersions);
            logger.Info(responseString);
            List<string> lst = listVersions.Versions.Select(c => c.Key).ToList();
            return listVersions;
        }
        public string DisplayImage(string key)
        {
            AmazonS3Client _amazonS3 = new AmazonS3Client(ConfigurationHelper.AmazoneAccessKey, ConfigurationHelper.AmazoneSecretAccessKey, new AmazonS3Config() { ServiceURL = ConfigurationHelper.AmazoneURL });
            string urlString = _amazonS3.GetPreSignedURL(new GetPreSignedUrlRequest()
            {
                BucketName = ConfigurationHelper.AmazoneBucket,
                Key = key,
                Expires = DateTime.UtcNow.AddHours(10)
            });
            string loggerFile = "AmazoneS3/Response";
            ILogger logger = LoggerFactory.CreateLogger( loggerFile);
            string responseString = JsonConvert.SerializeObject(urlString);
            logger.Info(responseString);
            return urlString;
        }
        public async Task<Stream> GetFile(string webRootPath, string key)
        {

            try
            {
                key = key.Replace("//", "\\");
                key = key.Replace("\\\\", "\\");
                AmazonS3Client _amazonS3 = new AmazonS3Client(ConfigurationHelper.AmazoneAccessKey, ConfigurationHelper.AmazoneSecretAccessKey, new AmazonS3Config() { ServiceURL = ConfigurationHelper.AmazoneURL });

                GetObjectResponse response = await _amazonS3.GetObjectAsync(ConfigurationHelper.AmazoneBucket, key);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return response.ResponseStream;
                else
                    return null;

            }
            catch (AmazonS3Exception ex)
            {

              
                string loggerFile =  "AmazoneS3/Excpetion";
                ILogger logger = LoggerFactory.CreateLogger( loggerFile);
                logger.Error(ex);
                return null;
            }
            catch (Exception ex)
            {
              
                string loggerFile = "AmazoneS3/Excpetion";
                ILogger logger = LoggerFactory.CreateLogger( loggerFile);
                logger.Error(ex);
                return null;
            }
        }
        public async Task<bool> DeleteFile(string key)
        {
            try
            {
                AmazonS3Client _amazonS3 = new AmazonS3Client(ConfigurationHelper.AmazoneAccessKey, ConfigurationHelper.AmazoneSecretAccessKey, new AmazonS3Config() { ServiceURL = ConfigurationHelper.AmazoneURL });

                DeleteObjectResponse response = await _amazonS3.DeleteObjectAsync(ConfigurationHelper.AmazoneBucket, key);
                string loggerFile = "AmazoneS3/Response";
                ILogger logger = LoggerFactory.CreateLogger( loggerFile);
                string responseString = JsonConvert.SerializeObject(response);
                logger.Info(responseString);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.NoContent)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
             
                string loggerFile =  "AmazoneS3/Excpetion";
                ILogger logger = LoggerFactory.CreateLogger( loggerFile);
                logger.Error(ex);
            }
            return false;

        }
        public async Task<int> DirectoryFilesAsync(string Prefix , string fullPath)
        {
            int count = -1;
            try
            {
                AmazonS3Client _amazonS3 = new AmazonS3Client(ConfigurationHelper.AmazoneAccessKey, ConfigurationHelper.AmazoneSecretAccessKey, new AmazonS3Config() { ServiceURL = ConfigurationHelper.AmazoneURL });


                var request = new ListObjectsV2Request
                {
                    BucketName = ConfigurationHelper.AmazoneBucket,
                    Prefix = Prefix,
                    MaxKeys = 1000
                };

                var response = await _amazonS3.ListObjectsV2Async(request);
                string loggerFile = "AmazoneS3/Response";
                ILogger logger = LoggerFactory.CreateLogger( loggerFile);
                string responseString = JsonConvert.SerializeObject(response);
                logger.Info(responseString);
                count = response.S3Objects.Count;
            }
            catch (AmazonS3Exception ex)
            {


                string loggerFile = "AmazoneS3/Excpetion";
                ILogger logger = LoggerFactory.CreateLogger( loggerFile);
                logger.Error(ex);
            }
            catch (Exception ex)
            {

                string loggerFile = "AmazoneS3/Excpetion";
                ILogger logger = LoggerFactory.CreateLogger( loggerFile);
                logger.Error(ex);
            }
            return count;
        }
        private async Task<bool>UploadToAmazom (MemoryStream fileStream , string fileName)
        {
            AmazonS3Client _amazonS3 = new AmazonS3Client(ConfigurationHelper.AmazoneAccessKey, ConfigurationHelper.AmazoneSecretAccessKey, new AmazonS3Config() { ServiceURL = ConfigurationHelper.AmazoneURL });
            PutObjectRequest request = new PutObjectRequest()
            {
                InputStream = fileStream,
                BucketName = ConfigurationHelper.AmazoneBucket,
                Key = fileName
            };

            PutObjectResponse amazoneResponse = await _amazonS3.PutObjectAsync(request);

            //log response

            string loggerFile = "AmazoneS3/Response";
            ILogger logger = LoggerFactory.CreateLogger( loggerFile);
            string responseString = JsonConvert.SerializeObject(amazoneResponse);
            logger.Info(responseString);

            if (amazoneResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
      
       
        public async Task<bool> CheckFileExists(string webRootPath, string key)
        {
            key = key.Replace("/", "\\");
            IStorageFactory fileConnector = StorageFactory.CreateFileConnetor();
            var result = await GetFile(webRootPath, key);
            if (result != null)
            {
                return true;
            }
            return false;
        }

    }
}

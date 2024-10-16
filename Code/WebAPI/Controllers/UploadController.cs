using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Helpers;
using Loggers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Upload")]
    public class UploadController : Controller
    {

        private IWebHostEnvironment _hostingEnvironment;
        private readonly ManageResources _manageRes;
        private readonly IFileService _fileService;
        public UploadController(IWebHostEnvironment hostingEnvironment , IFileService fileService)
        {
            _hostingEnvironment = hostingEnvironment;
            _manageRes = new ManageResources();
            _fileService = fileService;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult>  UploadFile()
        {
            ResultDTO result = new ResultDTO();
            try
            {
                var file = Request.Form.Files[0];
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string returnPath = await _fileService.Upload(file, _hostingEnvironment.WebRootPath, "uploads", fileName);
                if (returnPath != null)
                {
                    result.Results = returnPath;
                }
             
                
               
                return Ok(result);
            }
            catch (Exception ex)
            {                  
                Task.Run(()=> {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });                
                result.Errors.Add(_manageRes.GetErrorByName("UploadFailed"));
                return Ok(result);
            }
        }
        [HttpPost("UploadPDFImages/{documentPrefix}"), DisableRequestSizeLimit]
        public async Task<ActionResult> UploadPDFImagesFile(string documentPrefix)
        {
            ResultDTO result = new ResultDTO();
            try
            {

                string folderName = "uploads";
                string documentFolderName = "docs";
                string documentsFolderUrl = Path.Combine(folderName, documentFolderName);
              
                string uploadsPath = Path.Combine( documentsFolderUrl , "doc_" + documentPrefix);

                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    if (Request.Form.Files[i].Length > 0)
                    {
                        string imageFileName = "pdf_page_" + (i + 1).ToString() + ".jpeg"; ;

                        string returnPath = await _fileService.Upload(Request.Form.Files[i], _hostingEnvironment.WebRootPath, uploadsPath, imageFileName);
                        if (returnPath != null)
                        {
                            result.Results = returnPath;
                        }

                    }
                }

                result.Results = folderName;
                return Ok(result);
            }
            catch (Exception ex)
            {
                Task.Run(() => {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
                result.Errors.Add(_manageRes.GetErrorByName("UploadFailed"));
                return Ok(result);
            }
        }


        [HttpGet("upload-list")]
        public async Task<ActionResult> GetUploadList()
        {
            var result = await _fileService.FilesList();
            return Ok(result);

        }
        [HttpGet("downloads")]
        public async Task<ActionResult> DownloadFile([FromQuery] string Key)
        {
            
            var result =  await _fileService.GetFile(Key);
            return Ok(result);

        }

    }

}
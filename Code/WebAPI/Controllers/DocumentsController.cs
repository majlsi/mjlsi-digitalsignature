using Helpers;
using Loggers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/Documents")]
    public class DocumentsController : BaseController
    {
        private readonly Services.IDocumentService _documentService;
        private SecurityHelper _securityHelper;
        private PdfHelper _pdfHelper;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly Services.IDocumentPageService _documentPageService;
        private readonly Services.IDocumentUserService _documentUserService;
        private readonly Services.IDocumentFieldService _documentFieldService;
        private readonly Services.IUserService _userService;
        private readonly Services.IFieldTypeService _fieldTypeService;
        private readonly ManageResources _manageRes;
        private readonly Services.IFileService _fileService;
        public DocumentsController(Services.IDocumentService DocumentService, SecurityHelper securityHelper, PdfHelper pdfHelper, IWebHostEnvironment hostingEnvironment,
           Services.IDocumentPageService DocumentPageService,
           Services.IDocumentUserService DocumentUserService,
           Services.IDocumentFieldService DocumentFieldService,
           Services.IUserService UserService,
           Services.IFieldTypeService FieldTypeService,
           Services.IFileService fileService


           )
        {
            _documentService = DocumentService;
            _documentPageService = DocumentPageService;
            _documentUserService = DocumentUserService;
            _documentFieldService = DocumentFieldService;
            _userService = UserService;
            _fieldTypeService = FieldTypeService;
            _securityHelper = securityHelper;
            _pdfHelper = pdfHelper;
            _hostingEnvironment = hostingEnvironment;
            _manageRes = new ManageResources();
            _fileService = fileService;
        }

        // GET api/Documents
        public IActionResult GetDocuments()
        {

            ResultDTO result = new ResultDTO();
            List<Document> documents = _documentService.GetAll();
            result.Results = documents;
            return Ok(result);
        }


        // GET api/Documents/5
        [HttpGet("{id}")]
        public IActionResult GetDocument(int id)
        {
            ResultDTO result = new ResultDTO();
            Document document = _documentService.GetDocument(id);
            if (document == null)
            {
                return NotFound();
            }
            result.Results = document;
            return Ok(result);
        }

        // PUT api/Documents/5
        [HttpPut("{id}")]
        public IActionResult PutDocument(int id, [FromBody] Document document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != document.DocumentID)
            {
                return BadRequest();
            }

            try
            {
                _documentService.UpdateDocument(document);
                _documentService.SaveDocument();
            }
            catch (Exception ex)
            {
                Task.Run(() =>
                {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
                if (!DocumentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode((int)HttpStatusCode.NoContent);
        }

        // POST api/Documents
        [HttpPost]
        public async Task<IActionResult> PostDocument([FromBody] DocumentDTO documentDTO)
        {
            ResultDTO result = new ResultDTO();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string folderName = "uploads";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string pdfFilePath = Path.Combine(webRootPath, documentDTO.OriginalDocumentUrl);
            ErrorDTO error = await _documentService.ValidateDocument(documentDTO, webRootPath, documentDTO.OriginalDocumentUrl);
            if (error.ErrorMessageEN != null)
            {
                result.Errors.Add(error);
                return NotFound(result);
            }

            string documentsPath = Path.Combine(folderName, "docs");
            Document document = new Document();
            document.OriginalDocumentUrl = documentDTO.OriginalDocumentUrl;
            document.ApplicationID = _securityHelper.getAppIDFromToken();
            document.IsApproval = documentDTO.IsApproval ? true : false;
            string documentPrefix = DateTime.Now.ToString("yyyyMMddHHmmss");
            Document createdDocument = _documentService.CreateDocumentPagesWithFields(documentPrefix, documentDTO, document, documentsPath, webRootPath, ConfigurationHelper.SplitAPIURL, ConfigurationHelper.SplitActionName);
            _documentService.SaveDocument();
            result.Results = createdDocument;
            return Ok(result);
        }
        [HttpPost("documentFromImage")]
        public async Task<ActionResult> PostDocumentFromImage([FromBody] DocumentDTO documentDTO)
        {
            ResultDTO result = new ResultDTO();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string folderName = "uploads";
            string webRootPath = _hostingEnvironment.WebRootPath;
            ErrorDTO error = await _documentService.ValidateDocument(documentDTO, webRootPath, documentDTO.OriginalDocumentUrl);
            if (error.ErrorMessageEN != null)
            {
                result.Errors.Add(error);
                return NotFound(result);
            }
            string documentsPath = Path.Combine(folderName, "docs");
            Document document = new Document();
            document.OriginalDocumentUrl = documentDTO.OriginalDocumentUrl;
            document.ApplicationID = _securityHelper.getAppIDFromToken();
            document.ReturnURL = documentDTO.ReturnURL;
            Document createdDocument = await _documentService.CreateDocumentPagesFromImagesWithFields(documentDTO.DocumentPrefix, documentDTO, document, documentsPath, webRootPath);
            _documentService.SaveDocument();
            result.Results = createdDocument;

            return Ok(result);
        }

        // DELETE api/Documents/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDocument(int id)
        {
            ResultDTO result = new ResultDTO();
            Document document = _documentService.GetDocument(id);
            if (document == null)
            {
                return NotFound();
            }
            _documentService.DeleteDocument(document);
            _documentService.SaveDocument();
            result.Results = document;
            return Ok(result);
        }

        // GET api/Documents/documentPages/5
        [HttpGet("documentPages/{langCode}")]
        public IActionResult GetDocumentPages(string LangCode, [FromQuery] int timeZone)
        {
            ResultDTO result = new ResultDTO();
            string LanguageCode = "ar-SA";
            if (LangCode == "en")
            {
                LanguageCode = "en-US";
            }
            int? currentUser = null;
            if (ConfigurationHelper.IsThiqah)
            {
                currentUser = _securityHelper.getUserIDFromToken();
            }
            List<DocumentPageDTO> documentPages = _documentService.GetDocumentPages(_securityHelper.getDocumentIDFromToken(), LanguageCode, currentUser, timeZone);
            if (documentPages == null)
            {
                return NotFound();
            }
            if (documentPages.Count > 0)
            {
                documentPages[0].ReturnURL = _securityHelper.getReturnURLFromToken();
                documentPages[0].CallbackURL = _securityHelper.getCallbackURLFromToken();
            }
            result.Results = documentPages;
            return Ok(result);
        }
        [HttpPut("updateDocument")]
        public async Task<IActionResult> UpdateDocumentWithDetails([FromBody] DocumentDTO documentDTO)
        {
            ResultDTO result = new ResultDTO();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string folderName = "uploads";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string pdfFilePath = Path.Combine(webRootPath, documentDTO.OriginalDocumentUrl);
            ErrorDTO error = await _documentService.ValidateDocument(documentDTO, webRootPath, documentDTO.OriginalDocumentUrl);
            if (error.ErrorMessageEN != null)
            {
                result.Errors.Add(error);
                return NotFound(result);
            }

            string documentsPath = Path.Combine(folderName, "docs");

            string documentPrefix = DateTime.Now.ToString("yyyyMMddHHmmss");
            Document modifiedDocument = _documentService.UpdateDocumentWithFields(documentPrefix, documentDTO, documentsPath, webRootPath, ConfigurationHelper.SplitAPIURL, ConfigurationHelper.SplitActionName);
            _documentService.SaveDocument();
            result.Results = modifiedDocument;
            return Ok(result);
        }

        [HttpGet("documentBinaries/{id}")]
        public async Task<IActionResult> getDocumentBinaries([FromQuery] int timeZone, int id)
        {
            ResultDTO result = new ResultDTO();
            Document document = _documentService.GetDocument(id);
            string webRootPath = _hostingEnvironment.WebRootPath;
            List<DocumentPageDTO> documentPages = _documentService.GetDocumentPagesByAdmin(id);
            if (document == null)
            {
                return NotFound();
            }
            string documentUrl = string.Empty;

            var memory = await _documentService.insertSignaturesToDocument(webRootPath, document, documentPages, timeZone);

            return File(memory, "application/pdf");
        }

        private bool DocumentExists(int id)
        {
            Document document = _documentService.GetDocument(id);
            return document != null;
        }


    }
}
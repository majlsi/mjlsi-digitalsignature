using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Helpers;
using Loggers;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/DocumentFields")]
    public class DocumentFieldsController : BaseController
    {
        private readonly Services.IDocumentFieldService _documentFieldService;
        private readonly Services.IDocumentService _documentService;
        private SecurityHelper _securityHelper;

        public DocumentFieldsController(Services.IDocumentFieldService DocumentFieldService, SecurityHelper securityHelper, IDocumentService documentService)
        {
            _documentFieldService = DocumentFieldService;
            _securityHelper = securityHelper;
            _documentService = documentService;
        }

        // GET api/DocumentFields
        public IActionResult GetDocumentFields()
        {

            ResultDTO result = new ResultDTO();
            List<DocumentField> documentFields = _documentFieldService.GetAll();
            result.Results = documentFields;
            return Ok(result);

        }


        // GET api/DocumentFields/5
        [HttpGet("{id}")]
        public IActionResult GetDocumentField(int id)
        {
            ResultDTO result = new ResultDTO();
            DocumentField documentField = _documentFieldService.GetDocumentField(id);
            if (documentField == null)
            {
                return NotFound();
            }
            result.Results = documentField;
            return Ok(result);
        }

        // PUT api/DocumentFields/5
        [HttpPut("{id}")]
        public IActionResult PutDocumentField(int id, [FromBody] DocumentField documentField)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != documentField.DocumentFieldID)
            {
                return BadRequest();
            }

            try
            {
                _documentFieldService.UpdateDocumentField(documentField);
                _documentFieldService.SaveDocumentField();
            }
            catch (Exception ex)
            {
                Task.Run(() =>
                {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
                if (!DocumentFieldExists(id))
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

        // POST api/DocumentFields
        [HttpPost]
        public IActionResult PostDocumentField([FromBody] DocumentField documentField)
        {
            ResultDTO result = new ResultDTO();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _documentFieldService.CreateDocumentField(documentField);
            _documentFieldService.SaveDocumentField();
            result.Results = documentField;
            return Ok(result);
        }

        // DELETE api/DocumentFields/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDocumentField(int id)
        {
            ResultDTO result = new ResultDTO();
            DocumentField documentField = _documentFieldService.GetDocumentField(id);
            if (documentField == null)
            {
                return NotFound();
            }
            _documentFieldService.DeleteDocumentField(documentField);
            _documentFieldService.SaveDocumentField();
            result.Results = documentField;
            return Ok(result);
        }
        [HttpPut("sign/{id}")]
        public IActionResult Sign(int id, [FromBody] SignDTO signDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _documentFieldService.Sign(signDTO, id);
                _documentFieldService.SaveDocumentField();
                string CallbackURL = _securityHelper.getCallbackURLFromToken();
                int documentId = _securityHelper.getDocumentIDFromToken();
                string userEmail = _securityHelper.getUserEmailFromToken();
                Document document = _documentService.GetDocument(documentId);
                Task.Run(() => _documentFieldService.SendCallbackAction(CallbackURL, signDTO.DocumentFieldComment, documentId, userEmail, true, document.IsApproval));


            }
            catch (Exception ex)
            {
                Task.Run(() =>
                {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
                if (!DocumentFieldExists(id))
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

        [HttpPut("SaveAndSign/{id}")]
        public IActionResult SaveAndSign(int id, [FromBody] SignDTO signDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _documentFieldService.SaveAndSign(signDTO, id);
                _documentFieldService.SaveDocumentField();
                string CallbackURL = _securityHelper.getCallbackURLFromToken();
                int documentId = _securityHelper.getDocumentIDFromToken();
                string userEmail = _securityHelper.getUserEmailFromToken();
                Document document = _documentService.GetDocument(documentId);
                Task.Run(() => _documentFieldService.SendCallbackAction(CallbackURL, signDTO.DocumentFieldComment, documentId, userEmail, true, document.IsApproval));


            }
            catch (Exception ex)
            {
                Task.Run(() =>
                {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
                if (!DocumentFieldExists(id))
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

        [HttpPut("reject/{id}")]
        public IActionResult Reject(int id, [FromBody] RejectDTO rejectDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _documentFieldService.Reject(rejectDTO, id);
                _documentFieldService.SaveDocumentField();

                string CallbackURL = _securityHelper.getCallbackURLFromToken();
                int documentId = _securityHelper.getDocumentIDFromToken();
                string userEmail = _securityHelper.getUserEmailFromToken();
                Document document = _documentService.GetDocument(documentId);
                Task.Run(() => _documentFieldService.SendCallbackAction(CallbackURL, rejectDTO.DocumentFieldComment, documentId, userEmail, false, document.IsApproval));
            }
            catch (Exception ex)
            {
                Task.Run(() =>
                {
                    ILogger logger = LoggerFactory.CreateLogger( );
                    logger.Error(ex);
                });
                if (!DocumentFieldExists(id))
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

        [HttpPut("sign-delete/{id}")]
        public IActionResult DeleteSignature(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _documentFieldService.DeleteSignature(id);
                _documentFieldService.SaveDocumentField();
            }
            catch (Exception ex)
            {
                Task.Run(() =>
                {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
                if (!DocumentFieldExists(id))
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
        private bool DocumentFieldExists(int id)
        {
            DocumentField documentField = _documentFieldService.GetDocumentField(id);
            return documentField != null;
        }


    }
}
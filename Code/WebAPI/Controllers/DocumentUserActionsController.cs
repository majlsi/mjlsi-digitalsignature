using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Helpers;
using Loggers;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace WebAPI.Controllers
{
    [Route("api/DocumentUserActions")]
    public class DocumentUserActionsController : BaseController
    {
        private readonly Services.IDocumentUserActionService _documentUserActionService;
        private SecurityHelper _securityHelper;

        public DocumentUserActionsController(Services.IDocumentUserActionService DocumentUserActionService, SecurityHelper securityHelper)
        {
            _documentUserActionService = DocumentUserActionService;
            _securityHelper = securityHelper;

        }

        // GET api/DocumentUserActions
        public IActionResult GetDocumentUserActions()
        {
           
            ResultDTO result = new ResultDTO();
            List<DocumentUserAction> documentUserActions = _documentUserActionService.GetAll();
            result.Results = documentUserActions;
            return Ok(result);
        }


        // GET api/DocumentUserActions/5
        [HttpGet("{id}")]
        public IActionResult GetDocumentUserAction(int id)
        {
            ResultDTO result = new ResultDTO();
            DocumentUserAction documentUserAction = _documentUserActionService.GetDocumentUserAction(id);
            if (documentUserAction == null)
            {
                return NotFound();
            }
            result.Results = documentUserAction;
            return Ok(result);
        }

        // PUT api/DocumentUserActions/5
        [HttpPut("{id}")]
        public IActionResult PutDocumentUserAction(int id, [FromBody] DocumentUserAction documentUserAction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != documentUserAction.DocumentUserActionID)
            {
                return BadRequest();
            }

            try
            {
                _documentUserActionService.UpdateDocumentUserAction(documentUserAction);
                _documentUserActionService.SaveDocumentUserAction();
            }
            catch (Exception ex)
            {
                Task.Run(() => {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
                if (!DocumentUserActionExists(id))
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

        // POST api/DocumentUserActions
        [HttpPost]
        public IActionResult PostDocumentUserAction([FromBody] DocumentUserAction documentUserAction)
        {
            ResultDTO result = new ResultDTO();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _documentUserActionService.CreateDocumentUserAction(documentUserAction);
            _documentUserActionService.SaveDocumentUserAction();
            result.Results = documentUserAction;
            return Ok(result);
        }

        // DELETE api/DocumentUserActions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDocumentUserAction(int id)
        {
            ResultDTO result = new ResultDTO();
            DocumentUserAction documentUserAction = _documentUserActionService.GetDocumentUserAction(id);
            if (documentUserAction == null)
            {
                return NotFound();
            }
            _documentUserActionService.DeleteDocumentUserAction(documentUserAction);
            _documentUserActionService.SaveDocumentUserAction();
            result.Results = documentUserAction;
            return Ok(result);
        }


        private bool DocumentUserActionExists(int id)
        {
            DocumentUserAction documentUserAction = _documentUserActionService.GetDocumentUserAction(id);
            return documentUserAction != null;
        }


    }
}
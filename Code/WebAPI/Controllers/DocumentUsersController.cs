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
    [Route("api/DocumentUsers")]
    public class DocumentUsersController : BaseController
    {
        private readonly Services.IDocumentUserService _documentUserService;
        private SecurityHelper _securityHelper;

        public DocumentUsersController(Services.IDocumentUserService DocumentUserService, SecurityHelper securityHelper)
        {
            _documentUserService = DocumentUserService;
            _securityHelper = securityHelper;

        }

        // GET api/DocumentUsers
        public IActionResult GetDocumentUsers()
        {
            ResultDTO result = new ResultDTO();
            List<DocumentUser> documentUsers= _documentUserService.GetAll();
            result.Results = documentUsers;
            return Ok(result);
        }


        // GET api/DocumentUsers/5
        [HttpGet("{id}")]
        public IActionResult GetDocumentUser(int id)
        {
            ResultDTO result = new ResultDTO();
            DocumentUser documentUser = _documentUserService.GetDocumentUser(id);
            if (documentUser == null)
            {
                return NotFound();
            }
            result.Results = documentUser;
            return Ok(result);
        }

        // PUT api/DocumentUsers/5
        [HttpPut("{id}")]
        public IActionResult PutDocumentUser(int id, [FromBody] DocumentUser documentUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != documentUser.DocumentUserID)
            {
                return BadRequest();
            }

            try
            {
                _documentUserService.UpdateDocumentUser(documentUser);
                _documentUserService.SaveDocumentUser();
            }
            catch (Exception ex)
            {
                Task.Run(() => {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
                if (!DocumentUserExists(id))
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

        // POST api/DocumentUsers
        [HttpPost]
        public IActionResult PostDocumentUser([FromBody] DocumentUser documentUser)
        {
            ResultDTO result = new ResultDTO();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _documentUserService.CreateDocumentUser(documentUser);
            _documentUserService.SaveDocumentUser();
            result.Results = documentUser;
            return Ok(result);
        }

        // DELETE api/DocumentUsers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDocumentUser(int id)
        {
            ResultDTO result = new ResultDTO();
            DocumentUser documentUser = _documentUserService.GetDocumentUser(id);
            if (documentUser == null)
            {
                return NotFound();
            }
            _documentUserService.DeleteDocumentUser(documentUser);
            _documentUserService.SaveDocumentUser();
            result.Results = documentUser;
            return Ok(result);
        }

        private bool DocumentUserExists(int id)
        {
            DocumentUser documentUser = _documentUserService.GetDocumentUser(id);
            return documentUser != null;
        }


    }
}
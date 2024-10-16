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
    [Route("api/SignatureTypes")]
    public class SignatureTypesController : BaseController
    {
        private readonly Services.ISignatureTypeService _signatureTypeService;
        private SecurityHelper _securityHelper;

        public SignatureTypesController(Services.ISignatureTypeService SignatureTypeService, SecurityHelper securityHelper)
        {
            _signatureTypeService = SignatureTypeService;
            _securityHelper = securityHelper;

        }

        // GET api/SignatureTypes
        public IActionResult GetSignatureTypes()
        {
            ResultDTO result = new ResultDTO();
            List<SignatureType> signatureTypes = _signatureTypeService.GetAll();
            result.Results = signatureTypes;
            return Ok(result);       
        }


        // GET api/SignatureTypes/5
        [HttpGet("{id}")]
        public IActionResult GetSignatureType(int id)
        {
            ResultDTO result = new ResultDTO();
            SignatureType signatureType = _signatureTypeService.GetSignatureType(id);
            if (signatureType == null)
            {
                return NotFound();
            }
            result.Results = signatureType;
            return Ok(result);
        }

        // PUT api/SignatureTypes/5
        [HttpPut("{id}")]
        public IActionResult PutSignatureType(int id, [FromBody] SignatureType signatureType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != signatureType.SignatureTypeID)
            {
                return BadRequest();
            }

            try
            {
                _signatureTypeService.UpdateSignatureType(signatureType);
                _signatureTypeService.SaveSignatureType();
            }
            catch (Exception ex)
            {
                Task.Run(() => {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
                if (!SignatureTypeExists(id))
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

        // POST api/SignatureTypes
        [HttpPost]
        public IActionResult PostSignatureType([FromBody] SignatureType signatureType)
        {
            ResultDTO result = new ResultDTO();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _signatureTypeService.CreateSignatureType(signatureType);
            _signatureTypeService.SaveSignatureType();
            result.Results = signatureType;
            return Ok(result);
        }

        // DELETE api/SignatureTypes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSignatureType(int id)
        {
            ResultDTO result = new ResultDTO();
            SignatureType signatureType = _signatureTypeService.GetSignatureType(id);
            if (signatureType == null)
            {
                return NotFound();
            }
            _signatureTypeService.DeleteSignatureType(signatureType);
            _signatureTypeService.SaveSignatureType();
            result.Results = signatureType;
            return Ok(result);
        }


        private bool SignatureTypeExists(int id)
        {
            SignatureType signatureType = _signatureTypeService.GetSignatureType(id);
            return signatureType != null;
        }


    }
}
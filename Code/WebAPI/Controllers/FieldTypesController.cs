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
    [Route("api/FieldTypes")]
    public class FieldTypesController : BaseController
    {
        private readonly Services.IFieldTypeService _fieldTypeService;
        private SecurityHelper _securityHelper;

        public FieldTypesController(Services.IFieldTypeService FieldTypeService, SecurityHelper securityHelper)
        {
            _fieldTypeService = FieldTypeService;
            _securityHelper = securityHelper;

        }

        // GET api/FieldTypes
        public IActionResult GetFieldTypes()
        {
            ResultDTO result = new ResultDTO();
            List<FieldType> fieldTypes = _fieldTypeService.GetAll();
            result.Results = fieldTypes;
            return Ok(result);
        }


        // GET api/FieldTypes/5
        [HttpGet("{id}")]
        public IActionResult GetFieldType(int id)
        {
            ResultDTO result = new ResultDTO();
            FieldType fieldType = _fieldTypeService.GetFieldType(id);
            if (fieldType == null)
            {
                return NotFound();
            }
            result.Results = fieldType;
            return Ok(result);
        }

        // PUT api/FieldTypes/5
        [HttpPut("{id}")]
        public IActionResult PutFieldType(int id, [FromBody] FieldType fieldType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fieldType.FieldTypeID)
            {
                return BadRequest();
            }

            try
            {
                _fieldTypeService.UpdateFieldType(fieldType);
                _fieldTypeService.SaveFieldType();
            }
            catch (Exception ex)
            {
                Task.Run(() => {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
                if (!FieldTypeExists(id))
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

        // POST api/FieldTypes
        [HttpPost]
        public IActionResult PostFieldType([FromBody] FieldType fieldType)
        {
            ResultDTO result = new ResultDTO();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _fieldTypeService.CreateFieldType(fieldType);
            _fieldTypeService.SaveFieldType();
            result.Results = fieldType;
            return Ok(result);
        }

        // DELETE api/FieldTypes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFieldType(int id)
        {
            ResultDTO result = new ResultDTO();
            FieldType fieldType = _fieldTypeService.GetFieldType(id);
            if (fieldType == null)
            {
                return NotFound();
            }
            _fieldTypeService.DeleteFieldType(fieldType);
            _fieldTypeService.SaveFieldType();
            result.Results = fieldType;
            return Ok(result);
        }


        private bool FieldTypeExists(int id)
        {
            FieldType fieldType = _fieldTypeService.GetFieldType(id);
            return fieldType != null;
        }


    }
}
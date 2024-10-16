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
    [Route("api/ActionTypes")]
    public class ActionTypesController : BaseController
    {
        private readonly Services.IActionTypeService _actionTypeService;
        private SecurityHelper _securityHelper;

        public ActionTypesController(Services.IActionTypeService ActionTypeService, SecurityHelper securityHelper)
        {
            _actionTypeService = ActionTypeService;
            _securityHelper = securityHelper;

        }

        // GET api/ActionTypes
        public IActionResult GetActionTypes()
        {
      

            ResultDTO result = new ResultDTO();
            List<ActionType> actionTypes = _actionTypeService.GetAll();
            result.Results = actionTypes;
            return Ok(result);
            
        }


        // GET api/ActionTypes/5
        [HttpGet("{id}")]
        public IActionResult GetActionType(int id)
        {
            ResultDTO result = new ResultDTO();
            ActionType actionType = _actionTypeService.GetActionType(id);
            if (actionType == null)
            {
                return NotFound();
            }
            result.Results = actionType;
            return Ok(result);
        }

        // PUT api/ActionTypes/5
        [HttpPut("{id}")]
        public IActionResult PutActionType(int id, [FromBody] ActionType actionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actionType.ActionTypeID)
            {
                return BadRequest();
            }

            try
            {
                _actionTypeService.UpdateActionType(actionType);
                _actionTypeService.SaveActionType();
            }
            catch (Exception ex)
            {
                Task.Run(() => {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
                if (!ActionTypeExists(id))
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

        // POST api/ActionTypes
        [HttpPost]
        public IActionResult PostActionType([FromBody] ActionType actionType)
        {
            ResultDTO result = new ResultDTO();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _actionTypeService.CreateActionType(actionType);
            _actionTypeService.SaveActionType();
            result.Results = actionType;
            return Ok(result);
        }

        // DELETE api/ActionTypes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteActionType(int id)
        {
            ResultDTO result = new ResultDTO();
            ActionType actionType = _actionTypeService.GetActionType(id);
            if (actionType == null)
            {
                return NotFound();
            }
            _actionTypeService.DeleteActionType(actionType);
            _actionTypeService.SaveActionType();
            result.Results = actionType;
            return Ok(result);
        }


        private bool ActionTypeExists(int id)
        {
            ActionType actionType = _actionTypeService.GetActionType(id);
            return actionType != null;
        }


    }
}
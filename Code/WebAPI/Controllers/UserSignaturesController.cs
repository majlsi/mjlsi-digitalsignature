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
    [Route("api/UserSignatures")]
    public class UserSignaturesController : BaseController
    {
        private readonly Services.IUserSignatureService _userSignatureService;
        private SecurityHelper _securityHelper;

        public UserSignaturesController(Services.IUserSignatureService UserSignatureService, SecurityHelper securityHelper)
        {
            _userSignatureService = UserSignatureService;
            _securityHelper = securityHelper;

        }

        // GET api/UserSignatures
        public IActionResult GetUserSignatures()
        {
            int userId = _securityHelper.getUserIDFromToken();
            List<UserSignature> signatureTypes = _userSignatureService.GetUserSavedSignatures(userId);
            return Ok(signatureTypes);
        }


        // GET api/UserSignatures/5
        [HttpGet("{id}")]
        public IActionResult GetUserSignature(int id)
        {
            ResultDTO result = new ResultDTO();
            UserSignature userSignature = _userSignatureService.GetUserSignature(id);
            if (userSignature == null)
            {
                return NotFound();
            }
            result.Results = userSignature;
            return Ok(result);
        }

        // PUT api/UserSignatures/5
        [HttpPut("{id}")]
        public IActionResult PutUserSignature(int id, [FromBody] UserSignature userSignature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userSignature.UserSignatureID)
            {
                return BadRequest();
            }

            try
            {
                _userSignatureService.UpdateUserSignature(userSignature);
                _userSignatureService.SaveUserSignature();
            }
            catch (Exception ex)
            {
                Task.Run(() =>
                {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
                if (!UserSignatureExists(id))
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

        // POST api/UserSignatures
        [HttpPost]
        public IActionResult PostUserSignature([FromBody] UserSignature userSignature)
        {
            ResultDTO result = new ResultDTO();

            int currentUserId = _securityHelper.getUserIDFromToken();
            userSignature.UserID = currentUserId;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userSignatureService.CreateUserSignature(userSignature);
            _userSignatureService.SaveUserSignature();
            result.Results = userSignature;
            return Ok(result);
        }

        // DELETE api/UserSignatures/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUserSignature(int id)
        {
            ResultDTO result = new ResultDTO();
            UserSignature userSignature = _userSignatureService.GetUserSignature(id);
            if (userSignature == null)
            {
                return NotFound();
            }
            _userSignatureService.DeleteUserSignature(userSignature);
            _userSignatureService.SaveUserSignature();
            result.Results = userSignature;
            return Ok(result);
        }


        private bool UserSignatureExists(int id)
        {
            UserSignature userSignature = _userSignatureService.GetUserSignature(id);
            return userSignature != null;
        }


    }
}
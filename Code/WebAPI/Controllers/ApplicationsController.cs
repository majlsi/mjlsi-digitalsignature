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
    [Route("api/Applications")]
    public class ApplicationsController : BaseController
    {
        private readonly Services.IApplicationService _applicationService;
        private SecurityHelper _securityHelper;

        public ApplicationsController(Services.IApplicationService ApplicationService, SecurityHelper securityHelper)
        {
            _applicationService = ApplicationService;
            _securityHelper = securityHelper;

        }

        // GET api/Applications
        public IActionResult GetApplications()
        {

            ResultDTO result = new ResultDTO();
            List<Application> applications = _applicationService.GetAll();
            result.Results = applications;
            return Ok(result);
            
        }


        // GET api/Applications/5
        [HttpGet("{id}")]
        public IActionResult GetApplication(int id)
        {
            ResultDTO result = new ResultDTO();
            Application application = _applicationService.GetApplication(id);
            if (application == null)
            {
                return NotFound();
            }
            result.Results = application;
            return Ok(result);
        }

        // PUT api/Applications/5
        [HttpPut("{id}")]
        public IActionResult PutApplication(int id, [FromBody] Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != application.ApplicationID)
            {
                return BadRequest();
            }
            application.ApplicationPassword = _securityHelper.Md5Encryption(application.ApplicationPassword);

            try
            {
                _applicationService.UpdateApplication(application);
                _applicationService.SaveApplication();
            }
            catch (Exception ex)
            {
                Task.Run(() => {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
                if (!ApplicationExists(id))
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

        // POST api/Applications
        [HttpPost]
        public IActionResult PostApplication([FromBody] Application application)
        {
            ResultDTO result = new ResultDTO();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            application.ApplicationPassword = _securityHelper.Md5Encryption(application.ApplicationPassword);
            _applicationService.CreateApplication(application);
            _applicationService.SaveApplication();
            result.Results = application;
            return Ok(result);
        }

        // DELETE api/Applications/5
        [HttpDelete("{id}")]
        public IActionResult DeleteApplication(int id)
        {
            ResultDTO result = new ResultDTO();
            Application application = _applicationService.GetApplication(id);
            if (application == null)
            {
                return NotFound();
            }
            _applicationService.DeleteApplication(application);
            _applicationService.SaveApplication();
            result.Results = application;
            return Ok(result);
        }


        private bool ApplicationExists(int id)
        {
            Application application = _applicationService.GetApplication(id);
            return application != null;
        }


    }
}
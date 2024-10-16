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
	[Route("api/DocumentSignatureCodes")]
	[ApiController]
	public class DocumentSignatureCodeController : BaseController
    {
        private readonly IDocumentSignatureCodeService _documentSignatureCodeService;
        private readonly SecurityHelper _securityHelper;
        private readonly INotificationService _notificationService;
        public DocumentSignatureCodeController(
            IDocumentSignatureCodeService documentSignatureCodeService,
            SecurityHelper securityHelper,
            INotificationService notificationService)
        {
            _documentSignatureCodeService = documentSignatureCodeService;
            _securityHelper = securityHelper;
            _notificationService = notificationService;
        }

        // POST api/DocumentSignatureCodes/en
        [HttpPost("{lang}")]
        public IActionResult PostDocumentSignatureCode([FromBody] VerificationCodeGenerationDTO verificationCodeGenerationDTO,string lang)
        {
            int DocumentID = _securityHelper.getDocumentIDFromToken();
            int userID = _securityHelper.getUserIDFromToken();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentSignatureCode documentSignatureCode = new DocumentSignatureCode();
                documentSignatureCode.DocumentID = DocumentID;
                documentSignatureCode.UserID = userID;
                _documentSignatureCodeService.CreateDocumentSignatureCode(documentSignatureCode);
                _documentSignatureCodeService.SaveDocumentSignatureCode();
                if (ConfigurationHelper.EnableNotifications)
                {
                    _notificationService.SendVerificationCode(userID, verificationCodeGenerationDTO, documentSignatureCode, lang);
                }
            }
            catch (Exception ex)
            {
                Task.Run(() => {
                    ILogger logger = LoggerFactory.CreateLogger();
                    logger.Error(ex);
                });
                ResultDTO result = new ResultDTO();
                result.Errors.Add(new ErrorDTO
                {
                    ErrorMessageEN = "Code generation failed"
                });
                return Ok(result);
            }
            return Ok();
        }

        // POST api/DocumentSignatureCodes/VerifyCode
        [HttpPost("VerifyCode")]
        public IActionResult VerifyCode([FromBody] DocumentSignatureCodeDTO documentSignatureCodeDTO)
        {
            int UserID = _securityHelper.getUserIDFromToken();
            int DocumentID = _securityHelper.getDocumentIDFromToken();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResultDTO result = new ResultDTO();
			result.Results = _documentSignatureCodeService.VerifyDocumentSignatureCode(UserID,DocumentID, documentSignatureCodeDTO.VerificationCode);
            _documentSignatureCodeService.SaveDocumentSignatureCode();
            return Ok(result);
        }
    }
}

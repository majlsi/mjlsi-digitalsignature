using Data.Infrastructure;
using Data.Repositories;
using Helpers;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
	public class DocumentSignatureCodeService : IDocumentSignatureCodeService
    {
        private readonly IDocumentSignatureCodeRepository documentSignatureCodeRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly CodeGenerationHelper codeGeneratorHelper;

        public DocumentSignatureCodeService(
            IDocumentSignatureCodeRepository documentSignatureCodeRepository,
            IUnitOfWork unitOfWork,
            CodeGenerationHelper codeGeneratorHelper
            )
        {
            this.documentSignatureCodeRepository = documentSignatureCodeRepository;
            this.unitOfWork = unitOfWork;
            this.codeGeneratorHelper = codeGeneratorHelper;
        }

        #region IApplicationService Members


        public DocumentSignatureCode GetDocumentSignatureCode(int id)
        {
            var DocumentSignatureCode = documentSignatureCodeRepository.GetById(id);
            return DocumentSignatureCode;
        }

        public void CreateDocumentSignatureCode(DocumentSignatureCode DocumentSignatureCode)
        {
            DocumentSignatureCode.Document = null;
            DocumentSignatureCode.IsUsed = false;

            int codeLength = ConfigurationHelper.DocumentSignatureCodeLength;
            DocumentSignatureCode.VerificationCode = codeGeneratorHelper.GenerateRandomCode(codeLength);
            int codeExpirationDurationInMins = ConfigurationHelper.CodeExpirationDurationInMins;
            DocumentSignatureCode.VerificationCodeExpiration = DateTime.UtcNow.AddMinutes(codeExpirationDurationInMins);

            documentSignatureCodeRepository.Add(DocumentSignatureCode);
        }
        public List<DocumentSignatureCode> GetAll()
        {
            List<DocumentSignatureCode> DocumentSignatureCodes = documentSignatureCodeRepository.GetAll().ToList();
            return DocumentSignatureCodes;
        }
        public void UpdateDocumentSignatureCode(DocumentSignatureCode documentSignatureCode)
        {
            documentSignatureCodeRepository.Update(documentSignatureCode.DocumentSignatureCodeID, documentSignatureCode);
        }

        public void DeleteDocumentSignatureCode(DocumentSignatureCode documentSignatureCode)
        {
            documentSignatureCodeRepository.Delete(documentSignatureCode);
        }
        public void SaveDocumentSignatureCode()
        {
            unitOfWork.Commit();
        }

		public bool VerifyDocumentSignatureCode(int UserID,int DocumentID,string sentCode)
		{
            return documentSignatureCodeRepository.VerifyDocumentSignatureCode(UserID, DocumentID, sentCode);
		}
		#endregion
	}
}


using Data.Infrastructure;
using Data.Repositories;
using Helpers;
using Loggers;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Enums;

namespace Services
{

    public class DocumentFieldService : IDocumentFieldService
    {
        private readonly IDocumentFieldRepository DocumentFieldRepository;
        private readonly IDocumentPageRepository DocumentPageRepository;

        private readonly IUserSignatureRepository UserSignatureRepository;
        private readonly IUnitOfWork unitOfWork;
        private SecurityHelper _securityHelper;
        public DocumentFieldService(IDocumentFieldRepository DocumentFieldRepository, IDocumentPageRepository DocumentPageRepository, IUnitOfWork unitOfWork, SecurityHelper securityHelper ,IUserSignatureRepository UserSignatureRepository)
        {
            this.DocumentFieldRepository = DocumentFieldRepository;
            this.DocumentPageRepository = DocumentPageRepository;
            this.UserSignatureRepository = UserSignatureRepository;
            this.unitOfWork = unitOfWork;
            _securityHelper = securityHelper;

        }

        #region IApplicationService Members


        public DocumentField GetDocumentField(int id)
        {
            var DocumentField = DocumentFieldRepository.GetById(id);
            return DocumentField;
        }

        public void CreateDocumentField(DocumentField DocumentField)
        {
            DocumentField.DocumentPage = null;
            DocumentField.FieldType = null;
            DocumentFieldRepository.Add(DocumentField);
        }
        public List<DocumentField> GetAll()
        {
            List<DocumentField> DocumentFields = DocumentFieldRepository.GetAll().ToList();
            return DocumentFields;
        }
        public void UpdateDocumentField(DocumentField documentField)
        {
            documentField.DocumentPage = null;
            documentField.FieldType = null;
            documentField.User = null;
            DocumentFieldRepository.Update(documentField.DocumentFieldID, documentField);
        }
        public void Sign(SignDTO signDTO, int documentFieldID)
        {
            DocumentField documentField = DocumentFieldRepository.GetById(documentFieldID);
            int currentUserId = _securityHelper.getUserIDFromToken();
            int documentId = DocumentPageRepository.GetById(documentField.DocumentPageID).DocumentID;
            int currentDocumentId = _securityHelper.getDocumentIDFromToken();

            if (documentField.UserID == currentUserId && documentId == currentDocumentId)
            {
                if (signDTO.DocumentFieldComment != null)
                {
                    documentField.DocumentFieldComment = signDTO.DocumentFieldComment;
                }

                documentField.DocumentFieldValue = signDTO.DocumentFieldValue;
                if (signDTO.SignatureTypeID == 0)
                {
                    signDTO.SignatureTypeID = (int)SignatureTypeEnum.Draw;
                }
                documentField.SignatureTypeID = signDTO.SignatureTypeID;
                UpdateDocumentField(documentField);
            }

        }

        public void SaveAndSign(SignDTO signDTO, int documentFieldID)
        {
            this.Sign(signDTO,documentFieldID);
            UserSignature userSignature = new UserSignature();
            userSignature.UserID = _securityHelper.getUserIDFromToken();
            userSignature.SignatureValue = signDTO.DocumentFieldValue;
            UserSignatureRepository.Add(userSignature);
        }
        public void Reject(RejectDTO rejectDTO, int documentFieldID)
        {
            DocumentField documentField = DocumentFieldRepository.GetById(documentFieldID);
            int currentUserId = _securityHelper.getUserIDFromToken();
            int documentId = DocumentPageRepository.GetById(documentField.DocumentPageID).DocumentID;
            int currentDocumentId = _securityHelper.getDocumentIDFromToken();

            if (documentField.UserID == currentUserId && documentId == currentDocumentId)
            {
                if (rejectDTO.DocumentFieldComment != null)
                {
                    documentField.DocumentFieldComment = rejectDTO.DocumentFieldComment;
                }
                documentField.DocumentFieldValue = "false";
                UpdateDocumentField(documentField);
            }
        }
        public void DeleteSignature(int documentFieldID)
        {
            DocumentField documentField = DocumentFieldRepository.GetById(documentFieldID);
            int currentUserId = _securityHelper.getUserIDFromToken();
            int documentId = DocumentPageRepository.GetById(documentField.DocumentPageID).DocumentID;
            int currentDocumentId = _securityHelper.getDocumentIDFromToken();

            if (documentField.UserID == currentUserId && documentId == currentDocumentId)
            {

                documentField.DocumentFieldComment = null;
                documentField.DocumentFieldValue = null;
                UpdateDocumentField(documentField);
            }

        }

        public void DeleteDocumentField(DocumentField documentField)
        {
            DocumentFieldRepository.Delete(documentField);
        }

        public async Task SendCallbackAction(string CallBackURl, string DocumentFieldComment, int DocumentId, string UserEmail, bool IsSigned, bool IsApproval)
        {
            //Create Rest Client
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(CallBackURl, Method.Post);

            CallBackDTO _callback = new CallBackDTO();
            _callback.comment = DocumentFieldComment;
            _callback.document_id = DocumentId.ToString();
            _callback.is_signed = IsSigned;
            _callback.email = UserEmail;
            _callback.IsApproval = IsApproval;

            var json = JsonConvert.SerializeObject(_callback);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            try
            {
                RestResponse result = await client.ExecuteAsync(request);
            }
            catch (Exception ex)
            {
                ILogger logger = LoggerFactory.CreateLogger();
                logger.Error(ex);
            }
        }
        public void SaveDocumentField()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}

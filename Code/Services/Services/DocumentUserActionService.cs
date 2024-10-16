
using Data.Infrastructure;
using Data.Repositories;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Services
{

    public class DocumentUserActionService : IDocumentUserActionService
    {
        private readonly IDocumentUserActionRepository DocumentUserActionRepository;
        private readonly IUnitOfWork unitOfWork;

        public DocumentUserActionService(IDocumentUserActionRepository DocumentUserActionRepository, IUnitOfWork unitOfWork)
        {
            this.DocumentUserActionRepository = DocumentUserActionRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IApplicationService Members


        public DocumentUserAction GetDocumentUserAction(int id)
        {
            var DocumentUserAction = DocumentUserActionRepository.GetById(id);
            return DocumentUserAction;
        }

        public void CreateDocumentUserAction(DocumentUserAction DocumentUserAction)
        {
            DocumentUserAction.User = null;
            DocumentUserAction.ActionType = null;
            DocumentUserAction.Document = null;
            DocumentUserActionRepository.Add(DocumentUserAction);
        }
        public List<DocumentUserAction> GetAll()
        {
            List<DocumentUserAction> DocumentUserActions = DocumentUserActionRepository.GetAll().ToList();
            return DocumentUserActions;
        }
        public void UpdateDocumentUserAction(DocumentUserAction documentUserAction)
        {
            DocumentUserActionRepository.Update(documentUserAction.DocumentUserActionID, documentUserAction);
        }

        public void DeleteDocumentUserAction(DocumentUserAction documentUserAction)
        {
            DocumentUserActionRepository.Delete(documentUserAction);
        }
        public void SaveDocumentUserAction()
        {
            unitOfWork.Commit();       
        }



        #endregion
    }
}


using Data.Infrastructure;
using Data.Repositories;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Services
{

    public class DocumentPageService : IDocumentPageService
    {
        private readonly IDocumentPageRepository DocumentPageRepository;
        private readonly IUnitOfWork unitOfWork;

        public DocumentPageService(IDocumentPageRepository DocumentPageRepository, IUnitOfWork unitOfWork)
        {
            this.DocumentPageRepository = DocumentPageRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IApplicationService Members


        public DocumentPage GetDocumentPage(int id)
        {
            var DocumentPage = DocumentPageRepository.GetById(id);
            return DocumentPage;
        }

        public void CreateDocumentPage(DocumentPage DocumentPage)
        {
            DocumentPage.Document = null;
       
            DocumentPageRepository.Add(DocumentPage);
        }
        public List<DocumentPage> GetAll()
        {
            List<DocumentPage> DocumentPages = DocumentPageRepository.GetAll().ToList();
            return DocumentPages;
        }
        public void UpdateDocumentPage(DocumentPage documentPage)
        {
            DocumentPageRepository.Update(documentPage.DocumentPageID, documentPage);
        }

        public void DeleteDocumentPage(DocumentPage documentPage)
        {
            DocumentPageRepository.Delete(documentPage);
        }
        public void SaveDocumentPage()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}

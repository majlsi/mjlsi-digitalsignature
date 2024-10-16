
using Models;
using System.Collections.Generic;

namespace Services
{

    public interface IDocumentPageService
    {
        DocumentPage GetDocumentPage(int id);
        void CreateDocumentPage(DocumentPage DocumentPage);
        void SaveDocumentPage();
        List<DocumentPage> GetAll();
        void UpdateDocumentPage(DocumentPage documentPage);
        public void DeleteDocumentPage(DocumentPage documentPage);

    }
}

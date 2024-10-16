

using Models;
using System.Collections.Generic;

namespace Services
{

    public interface IDocumentUserActionService
    {
        DocumentUserAction GetDocumentUserAction(int id);
        void CreateDocumentUserAction(DocumentUserAction DocumentUserAction);
        List<DocumentUserAction> GetAll();

        void UpdateDocumentUserAction(DocumentUserAction documentUserAction);

        void DeleteDocumentUserAction(DocumentUserAction documentUserAction);
        
        void SaveDocumentUserAction();
    }
}

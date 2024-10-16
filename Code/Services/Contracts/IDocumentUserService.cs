
using Models;
using System.Collections.Generic;

namespace Services
{

    public interface IDocumentUserService
    {
        DocumentUser GetDocumentUser(int id);
        DocumentUser GetDocumentUser(int UserID,int DocumentID);
        void CreateDocumentUser(DocumentUser DocumentUser);
        void SaveDocumentUser();
        List<DocumentUser> GetAll();

        void UpdateDocumentUser(DocumentUser documentUser);


        void DeleteDocumentUser(DocumentUser documentUser);
	}
}

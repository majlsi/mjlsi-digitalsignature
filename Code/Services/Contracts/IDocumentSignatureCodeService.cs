using Models;
using System.Collections.Generic;

namespace Services
{
	public interface IDocumentSignatureCodeService
	{
        DocumentSignatureCode GetDocumentSignatureCode(int id);
        void CreateDocumentSignatureCode(DocumentSignatureCode DocumentSignatureCode);
        void SaveDocumentSignatureCode();
        List<DocumentSignatureCode> GetAll();
        void UpdateDocumentSignatureCode(DocumentSignatureCode documentSignatureCode);
        void DeleteDocumentSignatureCode(DocumentSignatureCode documentSignatureCode);
        bool VerifyDocumentSignatureCode(int UserID,int DocumentID,string sentCode);
    }
}

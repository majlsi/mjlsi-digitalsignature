
using Models;
using Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{

    public interface IDocumentFieldService
    {
        DocumentField GetDocumentField(int id);
        void CreateDocumentField(DocumentField DocumentField);
        void SaveDocumentField();
        List<DocumentField> GetAll();
        void UpdateDocumentField(DocumentField documentField);
        public void DeleteDocumentField(DocumentField documentField);
        public void Reject(RejectDTO rejectDTO, int documentFieldID);
        public void Sign(SignDTO signDTO, int documentFieldID);

        public void SaveAndSign(SignDTO signDTO, int documentFieldID);
        public void DeleteSignature(int documentFieldID);

        public Task SendCallbackAction(string CallBackURl, string DocumentFieldComment, int documentId, string UserEmail,bool IsSigned, bool IsApproval);

    }
}

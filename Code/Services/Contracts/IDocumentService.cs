

using Models;
using Models.DTO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Services
{

    public interface IDocumentService
    {
        Document GetDocument(int id);
        void CreateDocument(Document Document);
        void SaveDocument();
        List<Document> GetAll();
        void UpdateDocument(Document document);
        void DeleteDocument(Document document);
        List<DocumentPageDTO> GetDocumentPages(int documentID, string LangCode, int? currentUserID, int timeZone);

        List<DocumentPageDTO> GetDocumentPagesByAdmin(int documentID);
        Task<ErrorDTO> ValidateDocument(DocumentDTO documentDTO, string oraginalDoc, string root);
        Document CreateDocumentPagesWithFields(string documentPrefix, DocumentDTO documentDTO, Document document, string documentsPath, string webRootPath, string SplitAPIURL, string SplitActionName);
        Document UpdateDocumentWithFields(string documentPrefix, DocumentDTO documentDTO, string documentsPath, string webRootPath, string SplitAPIURL, string SplitActionName);
        Task<Document> CreateDocumentPagesFromImagesWithFields(string documentPrefix, DocumentDTO documentDTO, Document document, string documentsPath, string webRootPath);
        Task<MemoryStream> insertSignaturesToDocument(string webRootPath, Models.Document inputDocument, List<DocumentPageDTO> documentPages, int timeZone);
    }
}

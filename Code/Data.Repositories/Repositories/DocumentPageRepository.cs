
using Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{

    public class DocumentPageRepository : BaseRepository<DocumentPage>, IDocumentPageRepository
    {
        public DocumentPageRepository(IDbFactory dbFactory, Helpers.SecurityHelper securityHelper)
            : base(dbFactory, securityHelper) { }

        public List<DocumentPageDTO> GetAllByDocumentID(int documentID, int? currentUserId)
        {
            List<DocumentPageDTO> documentPages = DbContext.DocumentPages
                .Include(a => a.Document.DocumentUsers)
          .Where(d => d.DocumentID == documentID
          &&
          (currentUserId == null || d.Document.DocumentUsers.Any(u => u.UserID == currentUserId))).AsNoTracking()
             .Select(s => new DocumentPageDTO()
             {
                 DocumentPageID = s.DocumentPageID,
                 DocumentID = s.DocumentID,
                 PageNumber = s.PageNumber,
                 DocumentPageUrl = s.DocumentPageUrl,
                 DocumentFields = s.DocumentFields.Select(a => new DocumentFieldDTO()
                 {
                     DocumentFieldValue = a.DocumentFieldValue,
                     DocumentFieldID = a.DocumentFieldID,
                     DocumentPageID = a.DocumentPageID,
                     UserID = a.UserID,
                     XPosition = a.XPosition,
                     YPosition = a.YPosition,
                     FieldTypeID = a.FieldTypeID,
                     SignDate = a.ModificationDate ?? a.CreationDate

                 }).ToList()


             }).ToList();
            return documentPages;


        }
        public DocumentPage GetDocumentPageByPageNumber(int DocumentID, int PageNumber)
        {
            DocumentPage results = DbContext.DocumentPages.Where(a => a.DocumentID.Equals(DocumentID) && a.PageNumber.Equals(PageNumber)).FirstOrDefault();
            return results;
        }
    }
}

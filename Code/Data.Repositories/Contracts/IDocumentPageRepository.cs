

using Data.Infrastructure;
using Models;
using Models.DTO;
using System.Collections.Generic;

namespace Data.Repositories
{

    public interface IDocumentPageRepository : IRepository<DocumentPage>
    {

        List<DocumentPageDTO> GetAllByDocumentID(int documentID, int? currentUserId);
        DocumentPage GetDocumentPageByPageNumber(int DocumentID, int PageNumber);


    }



}

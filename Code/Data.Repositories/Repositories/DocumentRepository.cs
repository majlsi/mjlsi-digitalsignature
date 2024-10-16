
using Data.Infrastructure;
using Models;

namespace Data.Repositories
{

    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(IDbFactory dbFactory, Helpers.SecurityHelper securityHelper)
            : base(dbFactory, securityHelper) { }


    }


}

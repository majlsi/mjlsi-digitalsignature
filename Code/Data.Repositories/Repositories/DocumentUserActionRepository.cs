
using Data.Infrastructure;
using Models;

namespace Data.Repositories
{

    public class DocumentUserActionRepository : BaseRepository<DocumentUserAction>, IDocumentUserActionRepository
    {
        public DocumentUserActionRepository(IDbFactory dbFactory, Helpers.SecurityHelper securityHelper)
            : base(dbFactory, securityHelper) { }


    }


}

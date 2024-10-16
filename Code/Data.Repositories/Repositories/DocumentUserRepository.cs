
using Data.Infrastructure;
using Models;

namespace Data.Repositories
{

    public class DocumentUserRepository : BaseRepository<DocumentUser>, IDocumentUserRepository
    {
        public DocumentUserRepository(IDbFactory dbFactory, Helpers.SecurityHelper securityHelper)
            : base(dbFactory, securityHelper) { }


    }


}

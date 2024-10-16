
using Data.Infrastructure;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{

    public class DocumentFieldRepository : BaseRepository<DocumentField>, IDocumentFieldRepository
    {
        public DocumentFieldRepository(IDbFactory dbFactory, Helpers.SecurityHelper securityHelper)
            : base(dbFactory, securityHelper) { }

    
    }


}

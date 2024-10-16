
using Data.Infrastructure;
using Models;

namespace Data.Repositories
{

    public class SignatureTypeRepository : BaseRepository<SignatureType>, ISignatureTypeRepository
    {
        public SignatureTypeRepository(IDbFactory dbFactory, Helpers.SecurityHelper securityHelper)
            : base(dbFactory, securityHelper) { }


    }


}

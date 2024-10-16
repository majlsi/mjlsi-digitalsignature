
using Data.Infrastructure;
using Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;



namespace Data.Repositories
{

    public class UserSignatureRepository : BaseRepository<UserSignature>, IUserSignatureRepository
    {
        public UserSignatureRepository(IDbFactory dbFactory, Helpers.SecurityHelper securityHelper)
            : base(dbFactory, securityHelper) { }

        public List<UserSignature> GetUserSavedSignatures(int UserID)
        {
            List<UserSignature> results = DbContext.UserSignatures.Where(a => a.UserID.Equals(UserID)).AsNoTracking().ToList();
            return results;
        }


    }


}

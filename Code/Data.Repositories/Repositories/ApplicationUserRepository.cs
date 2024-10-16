
using Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;

namespace Data.Repositories
{

    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDbFactory dbFactory, Helpers.SecurityHelper securityHelper)
            : base(dbFactory, securityHelper) { }

   
        public ApplicationUser GetApplicationUserByUserID(int userID)
        {
            ApplicationUser applicationUser = DbContext.ApplicationUsers.Include(ap => ap.Application).Where(a => a.UserID.Equals(userID)).FirstOrDefault();
            return applicationUser;
        }
    }
}

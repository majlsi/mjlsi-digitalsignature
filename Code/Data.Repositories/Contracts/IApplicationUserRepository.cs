
using Data.Infrastructure;
using Models;

namespace Data.Repositories
{

    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetApplicationUserByUserID(int userID);
    }



}

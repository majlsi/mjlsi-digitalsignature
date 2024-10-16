
using Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;

namespace Data.Repositories
{

    public class ApplicationRepository : BaseRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(IDbFactory dbFactory, Helpers.SecurityHelper securityHelper)
            : base(dbFactory, securityHelper) { }

        public Application ApplicationLogin(string applicationEmail, string applicationPassword)
        {
            Application application = this.DbContext.Applications.Where(a => a.ApplicationEmail == applicationEmail.ToLower()).Where(c => c.ApplicationPassword == applicationPassword).AsNoTracking().FirstOrDefault();
            return application;
        }
    }


}

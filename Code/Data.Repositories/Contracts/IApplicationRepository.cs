
using Data.Infrastructure;
using Models;

namespace Data.Repositories
{

    public interface IApplicationRepository : IRepository<Application>
    {
        Application ApplicationLogin(string applicationEmail, string applicationPassword);
    }



}

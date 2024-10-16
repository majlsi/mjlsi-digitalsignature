using Data.Infrastructure;
using Models;
using Models.DTO;
using System.Collections.Generic;

namespace Data.Repositories
{

    public interface IUserRepository : IRepository<User>
    {
        User UserLogin(string username,int appID);
        User GetUserByEmail(string userEmail);
        User GetUser(string userEmail, int ApplicationId);
        PagedResult<User> GetAll(FilterModel<User> FilterObject);
        bool AllUserEmailsExist(List<string> DocumentUserEmails);
    }


}

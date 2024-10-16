
using Models;
using System.Collections.Generic;

namespace Services
{

    public interface IApplicationUserService
    {
        ApplicationUser GetApplicationUser(int id);
        void CreateApplicationUser(ApplicationUser ApplicationUser);
        List<ApplicationUser> GetAll();

        void UpdateApplicationUser(ApplicationUser documentUserAction);

       void DeleteApplicationUser(ApplicationUser documentUserAction);

       ApplicationUser GetApplicationUserByUserID(int userID);
        void SaveApplicationUser();
    }
}


using Data.Infrastructure;
using Data.Repositories;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Services
{

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository ApplicationUserRepository;
        private readonly IUnitOfWork unitOfWork;

        public ApplicationUserService(IApplicationUserRepository ApplicationUserRepository, IUnitOfWork unitOfWork)
        {
            this.ApplicationUserRepository = ApplicationUserRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IApplicationService Members


        public ApplicationUser GetApplicationUser(int id)
        {
            var ApplicationUser = ApplicationUserRepository.GetById(id);
            return ApplicationUser;
        }

        public void CreateApplicationUser(ApplicationUser ApplicationUser)
        {
            ApplicationUser.User = null;
            ApplicationUser.Application = null;

            ApplicationUserRepository.Add(ApplicationUser);
        }
        public List<ApplicationUser> GetAll()
        {
            List<ApplicationUser> ApplicationUsers = ApplicationUserRepository.GetAll().ToList();
            return ApplicationUsers;
        }
        public void UpdateApplicationUser(ApplicationUser documentUserAction)
        {
            ApplicationUserRepository.Update(documentUserAction.ApplicationUserID, documentUserAction);
        }

        public void DeleteApplicationUser(ApplicationUser documentUserAction)
        {
            ApplicationUserRepository.Delete(documentUserAction);
        }
        public ApplicationUser GetApplicationUserByUserID(int userID)
        {
            ApplicationUser applicationUser = ApplicationUserRepository.GetApplicationUserByUserID(userID);
            return applicationUser;
        }
        public void SaveApplicationUser()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}

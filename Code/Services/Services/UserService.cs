using Data.Infrastructure;
using Data.Repositories;
using Helpers;
using Models;
using Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly SecurityHelper securityHelper;
        private readonly IApplicationUserRepository applicationUserRepository;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork , IApplicationUserRepository applicationUserRepository, SecurityHelper securityHelper)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.securityHelper = securityHelper;
            this.applicationUserRepository = applicationUserRepository;
        }

        #region IApplicationService Members

        public List<User> GetAll()
        {
            List<User> Users = userRepository.GetAll().ToList();
            return Users;
        }
        public PagedResult<User> GetAll(int PageNumber, int PageSize, string SortBy = "", string SortDirection = "")
        {
            List<string> Includes = new List<string>();

            Models.DTO.PagedResult<User> Users = userRepository.GetAll(PageNumber, PageSize, Includes, SortBy, SortDirection);
            return Users;
        }
        public User GetUser(int id)
        {
            var application = userRepository.GetById(id);
            return application;
        }

        public void CreateUser(User user)
        {
         
            userRepository.Add(user);
        }

        public void UpdateUser(User user)
        {
            userRepository.Update(user.UserID,user);
        }

        public void DeleteUser(User user)
        {
            userRepository.Delete(user);
        }

        public void SaveUser()
        {
            unitOfWork.Commit();
        }

        public User UserLogin(string username, int appID)
        {
            User user = userRepository.UserLogin(username, appID);
           return user;
        }

         public User GetUserByEmail(string userEmail)
        {
            User user = userRepository.GetUserByEmail(userEmail);
            return user;
        }

        public User GetUser(string userEmail,int ApplicationId)
        {
            User user = userRepository.GetUser(userEmail,ApplicationId);
            return user;
        }
        public PagedResult<User> GetAll(FilterModel<User> FilterObject)
        {
            FilterObject.Includes = new List<string>();
            PagedResult<User> users = userRepository.GetAll(FilterObject);
            return users;
        }

        public void UpdateUserByEmail(int appId, UpdateUserDTO updateUserDTO)
        {

            User user = userRepository.GetUser(updateUserDTO.OldEmail, appId);

            if (user == null) //create user if not found
            {
                user = new User();
                user.UserEmail = updateUserDTO.Email;
                user.UserName = updateUserDTO.Email;
                user.UserPhoneNumber = updateUserDTO.Phone;
                user.UserPassword = securityHelper.Md5Encryption(ConfigurationHelper.NewUserDefaultPassword);
                user.FullName = updateUserDTO.Email;
                userRepository.Add(user);
                SaveUser();
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserID = user.UserID;
                applicationUser.ApplicationID = appId;
                applicationUserRepository.Add(applicationUser);
                SaveUser();
            }
            else
            {
                user.UserEmail = updateUserDTO.Email;
                user.UserName = updateUserDTO.Email;
                user.UserPhoneNumber = updateUserDTO.Phone;
                user.UserPassword = securityHelper.Md5Encryption(ConfigurationHelper.NewUserDefaultPassword);
                user.FullName = updateUserDTO.Email;
                userRepository.Update(user.UserID, user);
                SaveUser();
            }
        }

        #endregion
    }
}

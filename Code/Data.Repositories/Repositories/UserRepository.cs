using Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repositories
{


    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory, Helpers.SecurityHelper securityHelper)
            : base(dbFactory, securityHelper) { }

        public override IEnumerable<User> GetAll()
        {
           return DbContext.Users.AsNoTracking();
        }   

        public User UserLogin(string username, int appID)
        {
           
            User User = (from user in DbContext.Users

                         join applicationUser in DbContext.ApplicationUsers
                                           on user.UserID equals applicationUser.UserID
                                           where applicationUser.ApplicationID == appID && user.UserName == username.ToLower()
                         select user).AsNoTracking().FirstOrDefault();


            return User;

        }
        public User GetUserByEmail(string userEmail) 
        {
            return this.DbContext.Users.Where(u => u.UserEmail == userEmail.ToLower()).AsNoTracking().FirstOrDefault();
        }

        public User GetUser(string userEmail,int ApplicationId)
        {
         
            User User = (from user in DbContext.Users
                         join applicationUser in DbContext.ApplicationUsers
                                           on user.UserID equals applicationUser.UserID
                         where applicationUser.ApplicationID == ApplicationId && user.UserEmail == userEmail.ToLower()
                         select user

                         ).AsNoTracking().FirstOrDefault();


            return User;
     

        }

        public Models.DTO.PagedResult<User> GetAll(FilterModel<User> FilterObject)
        {
            Models.DTO.PagedResult<User> UserList = new Models.DTO.PagedResult<User>();
            Expression<Func<User, bool>> SearchCriteria = a => (

            (a.UserName.Contains(FilterObject.SearchObject.UserName) || string.IsNullOrEmpty(FilterObject.SearchObject.UserName))
            &&
            (a.FullName.Contains(FilterObject.SearchObject.FullName) || string.IsNullOrEmpty(FilterObject.SearchObject.FullName))



            );
            UserList = this.GetAll(FilterObject.PageNumber, FilterObject.PageSize, FilterObject.Includes, SearchCriteria, FilterObject.SortBy, FilterObject.SortDirection);
            return UserList;
        }
        public bool AllUserEmailsExist(List<string> DocumentUserEmails)
        {   
            var results = DbContext.Users.Where(a => DocumentUserEmails.Contains(a.UserEmail)).ToList();
            return results.Count() == DocumentUserEmails.Count();
        }
  
    }


}

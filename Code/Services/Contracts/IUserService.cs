using Data.Repositories;
using Models;
using Models.DTO;
using System.Collections.Generic;


namespace Services
{

    public interface IUserService
    {
        List<User> GetAll();
        PagedResult<User> GetAll(int PageNumber, int PageSize, string SortBy, string SortDirection);
        User GetUser(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        void SaveUser();
        User UserLogin(string username, int appID);
        User GetUserByEmail(string userEmail);

        User GetUser(string userEmail, int ApplicationId);
        PagedResult<User> GetAll(FilterModel<User> FilterObject);
        void UpdateUserByEmail(int appId, UpdateUserDTO updateUserDTO);
    }
}

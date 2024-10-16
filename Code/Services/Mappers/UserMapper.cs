
using Models;
using Models.DTO;

namespace Services.Mappers
{

    public class UserMapper : IUserMapper
    {
      
 
        public User MapUser(UserDTO userDTO)
        {
            User user = new User();
            user.UserName = userDTO.UserName;
            user.UserPassword = userDTO.UserPassword;
            user.FullName = userDTO.FullName;
            user.UserEmail = userDTO.UserEmail;
            user.UserPhoneNumber = userDTO.UserPhoneNumber;
            return user;
        }
        public UserDTO MapUser(User user)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.UserName = user.UserName;
            userDTO.UserPassword = user.UserPassword;
            userDTO.FullName = user.FullName;
            return userDTO;
        }

    }
}

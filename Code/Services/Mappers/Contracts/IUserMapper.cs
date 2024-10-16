

using Models;
using Models.DTO;
namespace Services
{

    public interface IUserMapper
    {

        public User MapUser(UserDTO userDTO);
        public UserDTO MapUser(User user);
    }
}

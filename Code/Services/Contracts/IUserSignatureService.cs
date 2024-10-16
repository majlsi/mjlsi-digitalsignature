using Models;
using System.Collections.Generic;

namespace Services
{
    public interface IUserSignatureService
    {

        UserSignature GetUserSignature(int id);
        void CreateUserSignature(UserSignature UserSignature);
        void UpdateUserSignature(UserSignature UserSignature);
        List<UserSignature> GetAll();

        public void DeleteUserSignature(UserSignature UserSignature);

        void SaveUserSignature();

        public List<UserSignature> GetUserSavedSignatures(int UserID);
    }
}

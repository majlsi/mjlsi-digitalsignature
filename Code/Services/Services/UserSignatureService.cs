
using Data.Infrastructure;
using Data.Repositories;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Services
{

    public class UserSignatureService : IUserSignatureService
    {
        private readonly IUserSignatureRepository UserSignatureRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserSignatureService(IUserSignatureRepository UserSignatureRepository, IUnitOfWork unitOfWork)
        {
            this.UserSignatureRepository = UserSignatureRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IUserSignatureService Members


        public UserSignature GetUserSignature(int id)
        {
            var UserSignature = UserSignatureRepository.GetById(id);
            return UserSignature;
        }

        public void CreateUserSignature(UserSignature UserSignature)
        {
            UserSignatureRepository.Add(UserSignature);
        }
        public List<UserSignature> GetAll()
        {
            List<UserSignature> UserSignatures = UserSignatureRepository.GetAll().ToList();
            return UserSignatures;
        }
        public void UpdateUserSignature(UserSignature UserSignature)
        {
            UserSignatureRepository.Update(UserSignature.UserSignatureID, UserSignature);
        }

        public void DeleteUserSignature(UserSignature UserSignature)
        {
            UserSignatureRepository.Delete(UserSignature);
        }
        public void SaveUserSignature()
        {
            unitOfWork.Commit();
        }


        public List<UserSignature> GetUserSavedSignatures(int UserID)
        {
            return UserSignatureRepository.GetUserSavedSignatures(UserID);
        }



        #endregion
    }
}

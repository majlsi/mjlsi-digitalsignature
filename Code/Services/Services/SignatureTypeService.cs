
using Data.Infrastructure;
using Data.Repositories;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Services
{

    public class SignatureTypeService : ISignatureTypeService
    {
        private readonly ISignatureTypeRepository SignatureTypeRepository;
        private readonly IUnitOfWork unitOfWork;

        public SignatureTypeService(ISignatureTypeRepository SignatureTypeRepository, IUnitOfWork unitOfWork)
        {
            this.SignatureTypeRepository = SignatureTypeRepository;
            this.unitOfWork = unitOfWork;
        }

        #region ISignatureTypeService Members


        public SignatureType GetSignatureType(int id)
        {
            var SignatureType = SignatureTypeRepository.GetById(id);
            return SignatureType;
        }

        public void CreateSignatureType(SignatureType SignatureType)
        {
            SignatureTypeRepository.Add(SignatureType);
        }
        public List<SignatureType> GetAll()
        {
            List<SignatureType> SignatureTypes = SignatureTypeRepository.GetAll().ToList();
            return SignatureTypes;
        }
        public void UpdateSignatureType(SignatureType SignatureType)
        {
            SignatureTypeRepository.Update(SignatureType.SignatureTypeID, SignatureType);
        }

        public void DeleteSignatureType(SignatureType SignatureType)
        {
            SignatureTypeRepository.Delete(SignatureType);
        }
        public void SaveSignatureType()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}

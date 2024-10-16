using Models;
using System.Collections.Generic;

namespace Services
{
    public interface ISignatureTypeService
    {

        SignatureType GetSignatureType(int id);
        void CreateSignatureType(SignatureType SignatureType);
        void UpdateSignatureType(SignatureType SignatureType);
        List<SignatureType> GetAll();

        public void DeleteSignatureType(SignatureType SignatureType);

        void SaveSignatureType();
    }
}

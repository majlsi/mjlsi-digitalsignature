using System.Collections.Generic;
using Data.Infrastructure;
using Models;


namespace Data.Repositories
{
    public interface IUserSignatureRepository : IRepository<UserSignature>
    {

        List<UserSignature> GetUserSavedSignatures(int UserID);

    }
}



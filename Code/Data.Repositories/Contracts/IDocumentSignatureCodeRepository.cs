using Data.Infrastructure;
using Models;

namespace Data.Repositories
{
	public interface IDocumentSignatureCodeRepository : IRepository<DocumentSignatureCode>
	{
		public bool VerifyDocumentSignatureCode(int UserID, int DocumentID, string sentCode);
    }
}

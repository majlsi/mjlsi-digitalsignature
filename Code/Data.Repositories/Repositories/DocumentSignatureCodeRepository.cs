using Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;

namespace Data.Repositories
{
	public class DocumentSignatureCodeRepository : BaseRepository<DocumentSignatureCode>, IDocumentSignatureCodeRepository
	{
		public DocumentSignatureCodeRepository(IDbFactory dbFactory, Helpers.SecurityHelper securityHelper)
			: base(dbFactory, securityHelper) { }

        public bool VerifyDocumentSignatureCode(int UserID, int DocumentID, string sentCode)
        {
            DocumentSignatureCode documentSignatureCode = DbContext.Set<DocumentSignatureCode>().AsNoTracking()
                .FirstOrDefault(dsc => dsc.UserID == UserID
                    && dsc.DocumentID == DocumentID
                    && dsc.VerificationCode == sentCode
                    && DateTime.Compare(dsc.VerificationCodeExpiration, DateTime.UtcNow) > 0
                    && !dsc.IsUsed
            );
            if (documentSignatureCode != null)
            {
                documentSignatureCode.IsUsed = true;
                this.Update(documentSignatureCode.DocumentSignatureCodeID, documentSignatureCode);
                return true;
            }
            return false;
        }
    }
}

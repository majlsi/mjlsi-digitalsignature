using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
	public class DocumentSignatureCode : BaseModel
	{
        public int DocumentSignatureCodeID { get; set; }
        public int DocumentID { get; set; }
        public string VerificationCode { get; set; }
        public DateTime VerificationCodeExpiration { get; set; }
        public bool IsUsed { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public Document Document { get; set; }
    }
}

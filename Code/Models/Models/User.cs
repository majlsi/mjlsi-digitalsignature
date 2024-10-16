
using System.Collections.Generic;

namespace Models
{
    public partial class User : BaseModel
    {
        
        public int UserID { get; set; }
        
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserPassword { get; set; }
        public bool IsActive { get; set; }

        public List<DocumentSignatureCode> DocumentSignatureCodes { get; set; }
        public List<DocumentUser> DocumentUsers { get; set; }
        public List<DocumentUserAction> DocumentUserActions { get; set; }
        public List<Application> Applications { get; set; }
        public List<UserSignature> UserSignatures { get; set; }
        
        public User()
        {
            DocumentSignatureCodes = new List<DocumentSignatureCode>();
            DocumentUsers = new List<DocumentUser>();
            DocumentUserActions = new List<DocumentUserAction>();
            Applications = new List<Application>();
            UserSignatures=new List<UserSignature>();
        }
    }
}

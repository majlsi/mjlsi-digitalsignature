
using System.Collections.Generic;
namespace Models
{
    public partial class Document : BaseModel
    {
        public int DocumentID { get; set; }
        public string OriginalDocumentUrl { get; set; }
        public int ApplicationID { get; set; }
        public string ReturnURL { get; set; }
        public bool IsApproval { get; set; }
        public Application Application { get; set; }
        public List<DocumentUser> DocumentUsers { get; set; }
        public List<DocumentPage> DocumentPages { get; set; }
        public List<DocumentSignatureCode> DocumentSignatureCodes { get; set; }
        public List<DocumentUserAction> DocumentUserActions { get; set; }
        

        public Document()
        {
          //  Application = new Application();
            DocumentPages = new List<DocumentPage>();
            DocumentUsers = new List<DocumentUser>();
            DocumentSignatureCodes = new List<DocumentSignatureCode>();
            DocumentUserActions = new List<DocumentUserAction>();
        }

    }
}

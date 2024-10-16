

using System.Collections.Generic;

namespace Models
{
    public partial class DocumentPage : BaseModel
    {
        public int DocumentPageID { get; set; }
        public int PageNumber { get; set; }
        public int DocumentID { get; set; }
        public string DocumentPageUrl { get; set; }
        public Document Document { get; set; }
        public List<DocumentField> DocumentFields { get; set; }

        public DocumentPage()
        {
           // Document = new Document();
            DocumentFields = new List<DocumentField>();

        }

    }
}

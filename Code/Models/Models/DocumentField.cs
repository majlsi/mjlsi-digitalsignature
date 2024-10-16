
namespace Models
{
    public partial class DocumentField : BaseModel
    {
        public int DocumentFieldID { get; set; }
        public int FieldTypeID { get; set; }
        public int DocumentPageID { get; set; }
        public int? UserID { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public string DocumentFieldValue { get; set; }
        public string DocumentFieldComment { get; set; }

        public FieldType FieldType { get; set; }
        public DocumentPage DocumentPage { get; set; }
        public User User { get; set; }

        public int? SignatureTypeID {get; set;}

        public SignatureType SignatureType {get; set;}


    }
}

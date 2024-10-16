
namespace Models
{
    public partial class DocumentUserAction : BaseModel
    {
        public int DocumentUserActionID { get; set; }
        public int UserID { get; set; }
        public int ActionTypeID { get; set; }
        public int DocumentID { get; set; }

        public Document Document { get; set; }
        public ActionType ActionType { get; set; }
        public User User { get; set; }

        public int? SignatureTypeID {get; set;}

        public SignatureType SignatureType {get; set;}

        public DocumentUserAction()
        {
            Document = new Document();
            ActionType = new ActionType();
            User = new User();
            SignatureType = new SignatureType();

        }
    }
}

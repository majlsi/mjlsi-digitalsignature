namespace Models
{
    public partial class DocumentUser : BaseModel
    {
        public int DocumentUserID { get; set; }
        public int UserID { get; set; }
        public int DocumentID { get; set; }
        public Document Document { get; set; }
        public User User { get; set; }
  

    }
}

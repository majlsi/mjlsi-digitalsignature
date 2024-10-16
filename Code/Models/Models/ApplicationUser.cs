
namespace Models
{
    public partial class ApplicationUser : BaseModel
    {
        
        public int ApplicationUserID { get; set; }

        public int UserID { get; set; }

        public int ApplicationID { get; set; }

        public Application Application { get; set; }

        public User User { get; set; }
        public ApplicationUser()
        {

        }
    }
}

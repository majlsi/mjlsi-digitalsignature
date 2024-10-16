

using System.Collections.Generic;


namespace Models
{
    public partial class Application : BaseModel
    {
        public int ApplicationID { get; set; }
        public int TimeZoneDifference { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationEmail { get; set; }
        public string ApplicationPassword { get; set; }
        public string ReturnURL { get; set; }
        public string CallbackURL { get; set; }
        public List<Document> Documents { get; set; }
        public List<ApplicationUser> ApplicationUsers { get; set; }

        public Application()
        {
            Documents = new List<Document>();
            ApplicationUsers = new List<ApplicationUser>();
        }

    }
}

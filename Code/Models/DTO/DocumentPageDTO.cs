
using System.Collections.Generic;
namespace Models.DTO
{
    public class DocumentPageDTO
    {
      
        public int DocumentPageID { get; set; }
        public int DocumentID { get; set; }
        public int PageNumber { get; set; }
        public string DocumentPageUrl { get; set; }

        public string ReturnURL { get; set; }
        public string CallbackURL { get; set; }


        public List<DocumentFieldDTO> DocumentFields { get; set; }
   

        public DocumentPageDTO()
        {
    
            DocumentFields = new List<DocumentFieldDTO>();
            

        }
    }
}

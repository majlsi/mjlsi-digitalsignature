
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Models.DTO
{
    public class DocumentDTO
    {
        [Required]
        public string OriginalDocumentUrl { get; set; }
      
     
        [Required]
        public List<DocumentFieldDTO> DocumentFields { get; set; }
        public int DocumentID { get; set; }

        public string DocumentPrefix { get; set; }
        public string ReturnURL { get; set; }
        public bool IsApproval { get; set; }
        public DocumentDTO()
        {
  
            DocumentFields = new List<DocumentFieldDTO>();
         
        }
    }
}


using System.ComponentModel.DataAnnotations;


namespace Models.DTO
{
    public class SignDTO
    {
        [Required]
        public string DocumentFieldValue{ get; set; }
    
        public string DocumentFieldComment { get; set; }

        [Required]
         public int SignatureTypeID { get; set; }

    }
}

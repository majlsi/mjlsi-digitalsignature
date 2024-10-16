
using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class ApplicationLoginDTO
    {
        [Required]
        public string ApplicationEmail { get; set; }
        [Required]
        public string ApplicationPassword { get; set; }
    }
}

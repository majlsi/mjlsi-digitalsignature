
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.DTO
{
    public class UserDTO
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserPassword { get; set; }

        public int ApplicationID { get; set; }
        public int UserID { get; set; }
        
        public string UserEmail { get; set; }
       
        public string UserPhoneNumber { get; set; }

    }
}

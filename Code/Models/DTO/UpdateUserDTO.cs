using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DTO
{
    public class UpdateUserDTO
    {
        public string OldEmail { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}

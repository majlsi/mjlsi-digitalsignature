using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DTO
{
    public class CallBackDTO
    {
        public string document_id { get; set; }
        public string email { get; set; }
        public bool is_signed { get; set; }
        public string comment { get; set; }
        public bool IsApproval { get; set; }
    }
}

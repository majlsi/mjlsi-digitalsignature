
using System;
namespace Models.DTO
{
    public class DocumentFieldDTO
    {
        public int DocumentFieldID { get; set; }
        public int FieldTypeID { get; set; }
        public int DocumentPageID { get; set; }
        public int? UserID { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public string DocumentFieldValue { get; set; }
        public string DocumentFieldComment { get; set; }
        public string DocumentFieldHtml { get; set; }
        public string DocumentFieldUserEmail { get; set; }
        public string DocumentFieldUserPhoneNumber { get; set; }
        public int PageNumber { get; set; }
        public DateTime? SignDate { get; set; }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BaseModel
    {
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public int? AssociatedCompanyID { get; set; }
    }
}

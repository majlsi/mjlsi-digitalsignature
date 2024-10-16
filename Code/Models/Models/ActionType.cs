
using System.Collections.Generic;

namespace Models
{
    
    public partial class ActionType : BaseModel
    {
        public int ActionTypeID { get; set; }
        public string ActionTypeName { get; set; }
        public List<DocumentUserAction> DocumentUserActions { get; set; }
        public ActionType()
        {
            DocumentUserActions = new List<DocumentUserAction>();
        }
    }
}

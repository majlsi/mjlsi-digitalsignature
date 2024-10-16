using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class NotificationType : BaseModel
    {
        public int NotificationTypeID { get; set; }
        public string TypeName { get; set; }
    }
}
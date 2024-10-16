using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.DTO
{
    public class NotificationActionDTO
    {
        public int NotificationActionID { get; set; }
        public string ActionName { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string Icon { get; set; }
    }
}
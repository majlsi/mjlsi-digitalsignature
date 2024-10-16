using System.Collections.Generic;

namespace Models
{
    public class NotificationAction : BaseModel
    {
        public int NotificationActionID { get; set; }
        public string ActionName { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string Icon { get; set; }
        public bool IsVisible { get; set; }
        public List<NotificationSetting> NotificationSettings { get; set; }
        public NotificationAction()
        {
            NotificationSettings = new List<NotificationSetting>();
        }
    }
}
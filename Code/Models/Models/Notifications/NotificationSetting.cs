namespace Models
{
    public class NotificationSetting : BaseModel
    {
        public int NotificationSettingID { get; set; }
        public int NotificationActionID { get; set; }
        public int NotificationTypeID { get; set; }
        public string TemplateName { get; set; }
        public string MessageTemplate { get; set; }
        public string Subject { get; set; }
        public string SubjectAr { get; set; }
        public NotificationType NotificationType { get; set; }
        public NotificationAction NotificationAction { get; set; }
    }
}
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Update_Notification_Setting_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "NotificationTypeID", "AssociatedCompanyID", "CreatedBy", "CreationDate", "ModificationDate", "ModifiedBy", "TypeName" },
                values: new object[] { 4, null, 0, null, null, 0, "MailGun" });

            migrationBuilder.InsertData(
                table: "NotificationSettings",
                columns: new[] { "NotificationSettingID", "AssociatedCompanyID", "CreatedBy", "CreationDate", "MessageTemplate", "ModificationDate", "ModifiedBy", "NotificationActionID", "NotificationTypeID", "Subject", "SubjectAr", "TemplateName" },
                values: new object[] { 6, null, 0, null, null, null, 0, 1, 4, "Verify signature code", "كود تاكيد التوقيع", "SendVerificationCodeEmail" });

            migrationBuilder.InsertData(
                table: "NotificationSettings",
                columns: new[] { "NotificationSettingID", "AssociatedCompanyID", "CreatedBy", "CreationDate", "MessageTemplate", "ModificationDate", "ModifiedBy", "NotificationActionID", "NotificationTypeID", "Subject", "SubjectAr", "TemplateName" },
                values: new object[] { 7, null, 0, null, null, null, 0, 3, 4, "Verify signature code", "كود تاكيد التوقيع", "SendVerificationCodeEmail" });

            migrationBuilder.InsertData(
                table: "NotificationSettings",
                columns: new[] { "NotificationSettingID", "AssociatedCompanyID", "CreatedBy", "CreationDate", "MessageTemplate", "ModificationDate", "ModifiedBy", "NotificationActionID", "NotificationTypeID", "Subject", "SubjectAr", "TemplateName" },
                values: new object[] { 8, null, 0, null, null, null, 0, 4, 4, "User Registration", "User Registration ar", "UserRegistration" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationSettings",
                keyColumn: "NotificationSettingID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "NotificationSettings",
                keyColumn: "NotificationSettingID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "NotificationSettings",
                keyColumn: "NotificationSettingID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "NotificationTypeID",
                keyValue: 4);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class change_app_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "applications",
                keyColumn: "ApplicationID",
                keyValue: 1,
                columns: new[] { "ApplicationEmail", "ApplicationName", "CallbackURL", "ReturnURL" },
                values: new object[] { "mjlsiAppTest1@gmail.com", "Mjlsi Signature", "https://mjlsi.com/app/BackEnd/public/api/v1/meetings/sign-mom-callback", "https://mjlsi.com/app" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "applications",
                keyColumn: "ApplicationID",
                keyValue: 1,
                columns: new[] { "ApplicationEmail", "ApplicationName", "CallbackURL", "ReturnURL" },
                values: new object[] { "test@etikal.sa", "Etikal Signature", "https://socpa-backend.thiqah.sa/Backend/api/jobs/contract-signature", "https://socpapp.thiqah.sa/app/requests/list" });
        }
    }
}

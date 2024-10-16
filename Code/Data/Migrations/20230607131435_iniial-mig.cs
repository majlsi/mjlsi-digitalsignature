using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class iniialmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "action_types",
                columns: table => new
                {
                    ActionTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActionTypeName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_action_types", x => x.ActionTypeID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "field_types",
                columns: table => new
                {
                    FieldTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FieldTypeName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_field_types", x => x.FieldTypeID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NotificationActions",
                columns: table => new
                {
                    NotificationActionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActionName = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TitleEn = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TitleAr = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Icon = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsVisible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationActions", x => x.NotificationActionID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    NotificationTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypeName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.NotificationTypeID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "signature_types",
                columns: table => new
                {
                    SignatureTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SignatureTypeName = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_signature_types", x => x.SignatureTypeID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserEmail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPhoneNumber = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPassword = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                columns: table => new
                {
                    NotificationSettingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NotificationActionID = table.Column<int>(type: "int", nullable: false),
                    NotificationTypeID = table.Column<int>(type: "int", nullable: false),
                    TemplateName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MessageTemplate = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Subject = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubjectAr = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.NotificationSettingID);
                    table.ForeignKey(
                        name: "FK_NotificationSettings_NotificationActions_NotificationActionID",
                        column: x => x.NotificationActionID,
                        principalTable: "NotificationActions",
                        principalColumn: "NotificationActionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationSettings_NotificationTypes_NotificationTypeID",
                        column: x => x.NotificationTypeID,
                        principalTable: "NotificationTypes",
                        principalColumn: "NotificationTypeID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "applications",
                columns: table => new
                {
                    ApplicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TimeZoneDifference = table.Column<int>(type: "int", nullable: false),
                    ApplicationName = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplicationEmail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplicationPassword = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReturnURL = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CallbackURL = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applications", x => x.ApplicationID);
                    table.ForeignKey(
                        name: "FK_applications_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_signatures",
                columns: table => new
                {
                    UserSignatureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    SignatureValue = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_signatures", x => x.UserSignatureID);
                    table.ForeignKey(
                        name: "FK_user_signatures_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "application_users",
                columns: table => new
                {
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ApplicationID = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_users", x => x.ApplicationUserID);
                    table.ForeignKey(
                        name: "FK_application_users_applications_ApplicationID",
                        column: x => x.ApplicationID,
                        principalTable: "applications",
                        principalColumn: "ApplicationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_application_users_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    DocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OriginalDocumentUrl = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplicationID = table.Column<int>(type: "int", nullable: false),
                    ReturnURL = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documents", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK_documents_applications_ApplicationID",
                        column: x => x.ApplicationID,
                        principalTable: "applications",
                        principalColumn: "ApplicationID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "document_pages",
                columns: table => new
                {
                    DocumentPageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PageNumber = table.Column<int>(type: "int", nullable: false),
                    DocumentID = table.Column<int>(type: "int", nullable: false),
                    DocumentPageUrl = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_pages", x => x.DocumentPageID);
                    table.ForeignKey(
                        name: "FK_document_pages_documents_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "documents",
                        principalColumn: "DocumentID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "document_signature_codes",
                columns: table => new
                {
                    DocumentSignatureCodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DocumentID = table.Column<int>(type: "int", nullable: false),
                    VerificationCode = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VerificationCodeExpiration = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsUsed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_signature_codes", x => x.DocumentSignatureCodeID);
                    table.ForeignKey(
                        name: "FK_document_signature_codes_documents_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "documents",
                        principalColumn: "DocumentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_document_signature_codes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "document_user_actions",
                columns: table => new
                {
                    DocumentUserActionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ActionTypeID = table.Column<int>(type: "int", nullable: false),
                    DocumentID = table.Column<int>(type: "int", nullable: false),
                    SignatureTypeID = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_user_actions", x => x.DocumentUserActionID);
                    table.ForeignKey(
                        name: "FK_document_user_actions_action_types_ActionTypeID",
                        column: x => x.ActionTypeID,
                        principalTable: "action_types",
                        principalColumn: "ActionTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_document_user_actions_documents_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "documents",
                        principalColumn: "DocumentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_document_user_actions_signature_types_SignatureTypeID",
                        column: x => x.SignatureTypeID,
                        principalTable: "signature_types",
                        principalColumn: "SignatureTypeID");
                    table.ForeignKey(
                        name: "FK_document_user_actions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "document_users",
                columns: table => new
                {
                    DocumentUserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    DocumentID = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_users", x => x.DocumentUserID);
                    table.ForeignKey(
                        name: "FK_document_users_documents_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "documents",
                        principalColumn: "DocumentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_document_users_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "document_fields",
                columns: table => new
                {
                    DocumentFieldID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FieldTypeID = table.Column<int>(type: "int", nullable: false),
                    DocumentPageID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    XPosition = table.Column<int>(type: "int", nullable: false),
                    YPosition = table.Column<int>(type: "int", nullable: false),
                    DocumentFieldValue = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DocumentFieldComment = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SignatureTypeID = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    AssociatedCompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_fields", x => x.DocumentFieldID);
                    table.ForeignKey(
                        name: "FK_document_fields_document_pages_DocumentPageID",
                        column: x => x.DocumentPageID,
                        principalTable: "document_pages",
                        principalColumn: "DocumentPageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_document_fields_field_types_FieldTypeID",
                        column: x => x.FieldTypeID,
                        principalTable: "field_types",
                        principalColumn: "FieldTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_document_fields_signature_types_SignatureTypeID",
                        column: x => x.SignatureTypeID,
                        principalTable: "signature_types",
                        principalColumn: "SignatureTypeID");
                    table.ForeignKey(
                        name: "FK_document_fields_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "NotificationActions",
                columns: new[] { "NotificationActionID", "ActionName", "AssociatedCompanyID", "CreatedBy", "CreationDate", "Icon", "IsVisible", "ModificationDate", "ModifiedBy", "TitleAr", "TitleEn" },
                values: new object[,]
                {
                    { 1, "SendVerificationCodeEmail", null, 0, null, null, false, null, 0, "كود تاكيد التوقيع", "Verify signature code" },
                    { 2, "SendVerificationCodeSMS", null, 0, null, null, false, null, 0, "كود تاكيد التوقيع", "Verify signature code" },
                    { 3, "SendVerificationCodeAll", null, 0, null, null, false, null, 0, "كود تاكيد التوقيع", "Verify signature code" },
                    { 4, "UserRegistration", null, 0, null, null, false, null, 0, "User Registration ar", "User Registration" }
                });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "NotificationTypeID", "AssociatedCompanyID", "CreatedBy", "CreationDate", "ModificationDate", "ModifiedBy", "TypeName" },
                values: new object[,]
                {
                    { 1, null, 0, null, null, 0, "Email" },
                    { 2, null, 0, null, null, 0, "SMS" },
                    { 3, null, 0, null, null, 0, "InAppNotification" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "AssociatedCompanyID", "CreatedBy", "CreationDate", "FullName", "IsActive", "ModificationDate", "ModifiedBy", "UserEmail", "UserName", "UserPassword", "UserPhoneNumber" },
                values: new object[] { 1, null, 0, null, " Admin", true, null, 0, "admin@admin.com", "admin", "ꉟ뺾", null });

            migrationBuilder.InsertData(
                table: "action_types",
                columns: new[] { "ActionTypeID", "ActionTypeName", "AssociatedCompanyID", "CreatedBy", "CreationDate", "ModificationDate", "ModifiedBy" },
                values: new object[,]
                {
                    { 1, "Viewed", null, 0, null, null, 0 },
                    { 2, "Signed", null, 0, null, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "applications",
                columns: new[] { "ApplicationID", "ApplicationEmail", "ApplicationName", "ApplicationPassword", "AssociatedCompanyID", "CallbackURL", "CreatedBy", "CreationDate", "ModificationDate", "ModifiedBy", "ReturnURL", "TimeZoneDifference", "UserID" },
                values: new object[] { 1, "test@etikal.sa", "Etikal Signature", "ꉟ뺾", null, "https://socpa-backend.thiqah.sa/Backend/api/jobs/contract-signature", 0, null, null, 0, "https://socpapp.thiqah.sa/app/requests/list", 0, null });

            migrationBuilder.InsertData(
                table: "field_types",
                columns: new[] { "FieldTypeID", "AssociatedCompanyID", "CreatedBy", "CreationDate", "FieldTypeName", "ModificationDate", "ModifiedBy" },
                values: new object[,]
                {
                    { 1, null, 0, null, "Signature", null, 0 },
                    { 2, null, 0, null, "SignatureButton", null, 0 },
                    { 3, null, 0, null, "Button", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "signature_types",
                columns: new[] { "SignatureTypeID", "AssociatedCompanyID", "CreatedBy", "CreationDate", "ModificationDate", "ModifiedBy", "SignatureTypeName" },
                values: new object[,]
                {
                    { 1, null, 0, null, null, 0, "Draw" },
                    { 2, null, 0, null, null, 0, "Upload" },
                    { 3, null, 0, null, null, 0, "Type" },
                    { 4, null, 0, null, null, 0, "Saved Signature" }
                });

            migrationBuilder.InsertData(
                table: "NotificationSettings",
                columns: new[] { "NotificationSettingID", "AssociatedCompanyID", "CreatedBy", "CreationDate", "MessageTemplate", "ModificationDate", "ModifiedBy", "NotificationActionID", "NotificationTypeID", "Subject", "SubjectAr", "TemplateName" },
                values: new object[,]
                {
                    { 1, null, 0, null, null, null, 0, 1, 1, "Verify signature code", "كود تاكيد التوقيع", "SendVerificationCodeEmail" },
                    { 2, null, 0, null, null, null, 0, 2, 2, "Verify signature code", "كود تاكيد التوقيع", "SendVerificationCodeSMS" },
                    { 3, null, 0, null, null, null, 0, 3, 1, "Verify signature code", "كود تاكيد التوقيع", "SendVerificationCodeEmail" },
                    { 4, null, 0, null, null, null, 0, 3, 2, "Verify signature code", "كود تاكيد التوقيع", "SendVerificationCodeSMS" },
                    { 5, null, 0, null, null, null, 0, 4, 1, "User Registration", "User Registration ar", "UserRegistration" }
                });

            migrationBuilder.InsertData(
                table: "application_users",
                columns: new[] { "ApplicationUserID", "ApplicationID", "AssociatedCompanyID", "CreatedBy", "CreationDate", "ModificationDate", "ModifiedBy", "UserID" },
                values: new object[] { 1, 1, null, 0, null, null, 0, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_application_users_ApplicationID",
                table: "application_users",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_application_users_UserID",
                table: "application_users",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_applications_UserID",
                table: "applications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_document_fields_DocumentPageID",
                table: "document_fields",
                column: "DocumentPageID");

            migrationBuilder.CreateIndex(
                name: "IX_document_fields_FieldTypeID",
                table: "document_fields",
                column: "FieldTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_document_fields_SignatureTypeID",
                table: "document_fields",
                column: "SignatureTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_document_fields_UserID",
                table: "document_fields",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_document_pages_DocumentID",
                table: "document_pages",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_document_signature_codes_DocumentID",
                table: "document_signature_codes",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_document_signature_codes_UserID",
                table: "document_signature_codes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_document_user_actions_ActionTypeID",
                table: "document_user_actions",
                column: "ActionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_document_user_actions_DocumentID",
                table: "document_user_actions",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_document_user_actions_SignatureTypeID",
                table: "document_user_actions",
                column: "SignatureTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_document_user_actions_UserID",
                table: "document_user_actions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_document_users_DocumentID",
                table: "document_users",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_document_users_UserID",
                table: "document_users",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_documents_ApplicationID",
                table: "documents",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_NotificationActionID",
                table: "NotificationSettings",
                column: "NotificationActionID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_NotificationTypeID",
                table: "NotificationSettings",
                column: "NotificationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_user_signatures_UserID",
                table: "user_signatures",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "application_users");

            migrationBuilder.DropTable(
                name: "document_fields");

            migrationBuilder.DropTable(
                name: "document_signature_codes");

            migrationBuilder.DropTable(
                name: "document_user_actions");

            migrationBuilder.DropTable(
                name: "document_users");

            migrationBuilder.DropTable(
                name: "NotificationSettings");

            migrationBuilder.DropTable(
                name: "user_signatures");

            migrationBuilder.DropTable(
                name: "document_pages");

            migrationBuilder.DropTable(
                name: "field_types");

            migrationBuilder.DropTable(
                name: "action_types");

            migrationBuilder.DropTable(
                name: "signature_types");

            migrationBuilder.DropTable(
                name: "NotificationActions");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "documents");

            migrationBuilder.DropTable(
                name: "applications");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

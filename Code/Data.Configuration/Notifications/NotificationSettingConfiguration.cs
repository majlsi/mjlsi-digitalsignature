using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using Models.Enums;
using System.Collections.Generic;


namespace Data.Configuration
{
    public class NotificationSettingConfiguration : IEntityTypeConfiguration<NotificationSetting>
    {
        public void Configure(EntityTypeBuilder<NotificationSetting> builder)
        {
            builder.Property(n => n.TemplateName).HasMaxLength(150);
            builder.Property(n => n.Subject).HasMaxLength(1000);
            builder.Property(n => n.SubjectAr).HasMaxLength(1000);
            builder.Property(n => n.MessageTemplate).HasMaxLength(4000);

            List<NotificationSetting> notificationSettings = new List<NotificationSetting>
            {
                new NotificationSetting()
                {
                    NotificationSettingID = 1,
                    NotificationActionID = (int)NotificationActionEnum.SendVerificationCodeEmail,
                    NotificationTypeID = (int)NotificationTypeEnum.Email,
                    TemplateName = "SendVerificationCodeEmail",
                    SubjectAr = "كود تاكيد التوقيع",
                    Subject ="Verify signature code"

                },
                new NotificationSetting()
                {
                    NotificationSettingID = 2,
                    NotificationActionID = (int)NotificationActionEnum.SendVerificationCodeSMS,
                    NotificationTypeID = (int)NotificationTypeEnum.SMS,
                    TemplateName = "SendVerificationCodeSMS",
                    SubjectAr = "كود تاكيد التوقيع",
                    Subject ="Verify signature code"
                },
                new NotificationSetting()
                {
                    NotificationSettingID = 3,
                    NotificationActionID = (int)NotificationActionEnum.SendVerificationCodeAll,
                    NotificationTypeID = (int)NotificationTypeEnum.Email,
                    TemplateName = "SendVerificationCodeEmail",
                    SubjectAr = "كود تاكيد التوقيع",
                    Subject ="Verify signature code"
                },
                  new NotificationSetting()
                {
                    NotificationSettingID = 4,
                    NotificationActionID = (int)NotificationActionEnum.SendVerificationCodeAll,
                    NotificationTypeID = (int)NotificationTypeEnum.SMS,
                    TemplateName = "SendVerificationCodeSMS",
                    SubjectAr = "كود تاكيد التوقيع",
                    Subject ="Verify signature code"
                },
                new NotificationSetting()
                {
                    NotificationSettingID = 5,
                    NotificationActionID = (int)NotificationActionEnum.UserRegistration,
                    NotificationTypeID = (int)NotificationTypeEnum.Email,
                    TemplateName = "UserRegistration",
                    SubjectAr="User Registration ar",
                    Subject ="User Registration"
                },
                new NotificationSetting()
                {
                    NotificationSettingID = 6,
                    NotificationActionID = (int)NotificationActionEnum.SendVerificationCodeEmail,
                    NotificationTypeID = (int)NotificationTypeEnum.MailGun,
                    TemplateName = "SendVerificationCodeEmail",
                    SubjectAr = "كود تاكيد التوقيع",
                    Subject ="Verify signature code"

                },
                new NotificationSetting()
                {
                    NotificationSettingID = 7,
                    NotificationActionID = (int)NotificationActionEnum.SendVerificationCodeAll,
                    NotificationTypeID = (int)NotificationTypeEnum.MailGun,
                    TemplateName = "SendVerificationCodeEmail",
                    SubjectAr = "كود تاكيد التوقيع",
                    Subject ="Verify signature code"
                },
                new NotificationSetting()
                {
                    NotificationSettingID = 8,
                    NotificationActionID = (int)NotificationActionEnum.UserRegistration,
                    NotificationTypeID = (int)NotificationTypeEnum.MailGun,
                    TemplateName = "UserRegistration",
                    SubjectAr="User Registration ar",
                    Subject ="User Registration"
                },

            };

            builder.HasData(notificationSettings);
        }
    }
}
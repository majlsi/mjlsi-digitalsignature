using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.Configuration
{
    public class NotificationActionConfiguration : IEntityTypeConfiguration<NotificationAction>
    {
        public void Configure(EntityTypeBuilder<NotificationAction> builder)
        {
            builder.Property(n => n.ActionName).HasMaxLength(1000);
            builder.Property(n => n.TitleAr).HasMaxLength(1000);
            builder.Property(n => n.TitleEn).HasMaxLength(1000);
            builder.Property(n => n.Icon).HasMaxLength(400);
            List<NotificationAction> actions = new List<NotificationAction>()
            {
                new NotificationAction()
                {
                    NotificationActionID = 1,
                    ActionName = "SendVerificationCodeEmail" ,
                    TitleAr =  "كود تاكيد التوقيع",
                    TitleEn = "Verify signature code",
                    IsVisible=false
                },
                new NotificationAction()
                {
                    NotificationActionID = 2,
                    ActionName = "SendVerificationCodeSMS" ,
                    TitleAr =  "كود تاكيد التوقيع",
                    TitleEn = "Verify signature code",
                    IsVisible=false
                },
                  new NotificationAction()
                {
                    NotificationActionID = 3,
                    ActionName = "SendVerificationCodeAll" ,
                    TitleAr =  "كود تاكيد التوقيع",
                    TitleEn = "Verify signature code",
                    IsVisible=false
                },
                new NotificationAction()
                {
                    NotificationActionID = 4,
                    ActionName = "UserRegistration" ,
                    TitleAr =  "User Registration ar",
                    TitleEn = "User Registration",
                    IsVisible=false
                },
            };

            builder.HasData(actions);
        }
    }
}
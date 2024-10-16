using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System.Collections.Generic;

namespace Data.Configuration
{
    public class NotificationTypeConfiguration : IEntityTypeConfiguration<NotificationType>
    {
        public void Configure(EntityTypeBuilder<NotificationType> builder)
        {
            builder.Property(n => n.TypeName).HasMaxLength(255);
            List<NotificationType> types = new List<NotificationType>()
            {
                new NotificationType()
                {
                    TypeName = "Email",
                    NotificationTypeID = 1
                },
                new NotificationType()
                {
                    TypeName = "SMS",
                    NotificationTypeID = 2
                },
                new NotificationType()
                {
                    TypeName = "InAppNotification",
                    NotificationTypeID = 3
                },
                new NotificationType()
                {
                    TypeName = "MailGun",
                    NotificationTypeID = 4
                },
            };
            builder.HasData(types);
        }
    }
}
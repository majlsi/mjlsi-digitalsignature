using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System.Collections.Generic;

namespace Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        
        public void Configure(EntityTypeBuilder<User> builder){
       
            builder.Property(u => u.FullName).IsRequired().HasMaxLength(250);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(150);
            builder.Property(u => u.UserEmail).HasMaxLength(255);
            builder.Property(u => u.UserPassword).IsRequired().HasMaxLength(200);
            builder.Property(u => u.UserPhoneNumber).HasMaxLength(150);



            List<User> lstUsers = new List<User>();

            User useSeed = new User();
            useSeed.FullName = " Admin";
            useSeed.UserEmail = "admin@admin.com";
            useSeed.UserName = "admin";
            useSeed.UserPassword = "ꉟ뺾";
            useSeed.IsActive = true;
            useSeed.UserID = 1;
            lstUsers.Add(useSeed);

            builder.HasData(lstUsers.ToArray());

        }
    }
}

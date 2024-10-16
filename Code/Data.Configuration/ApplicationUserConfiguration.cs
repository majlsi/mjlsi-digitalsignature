
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System.Collections.Generic;

namespace Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("application_users");
        
         
            List<ApplicationUser> ApplicationUsers = new List<ApplicationUser>();

            ApplicationUsers.Add(new ApplicationUser
            {
                ApplicationUserID = 1,
                ApplicationID = 1,
                 UserID=1
        


            });


            builder.HasData(ApplicationUsers.ToArray());

        }
    }
}

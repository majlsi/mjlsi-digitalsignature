
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System.Collections.Generic;

namespace Data.Configuration
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {

        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.Property(a => a.ApplicationName).IsRequired().HasMaxLength(250);
            builder.Property(a => a.ApplicationEmail).HasMaxLength(255);
            builder.Property(a => a.ApplicationPassword).IsRequired().HasMaxLength(200);
            builder.Property(a => a.TimeZoneDifference).IsRequired();
            builder.Property(a => a.ReturnURL).HasMaxLength(1000);
            builder.Property(a => a.CallbackURL).HasMaxLength(1000);

            builder.ToTable("applications");
            List<Application> Applications = new List<Application>();

            Applications.Add(new Application
            {
                ApplicationID = 1,
                ApplicationName = "Mjlsi Signature",
                ApplicationEmail= "mjlsiAppTest1@gmail.com",
                ApplicationPassword= "ꉟ뺾",
                ReturnURL= "https://mjlsi.com/app",
                CallbackURL= "https://mjlsi.com/app/BackEnd/public/api/v1/meetings/sign-mom-callback"
            });
       

            builder.HasData(Applications.ToArray());
        }
    }
}

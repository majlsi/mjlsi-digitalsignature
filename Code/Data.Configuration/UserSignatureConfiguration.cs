using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System.Collections.Generic;

namespace Data.Configuration
{
    public class UserSignatureConfiguration : IEntityTypeConfiguration<UserSignature>
    {

        public void Configure(EntityTypeBuilder<UserSignature> builder)
        {
            builder.ToTable("user_signatures");
            builder.Property(a => a.SignatureValue).HasColumnType("text");

        }
    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System.Collections.Generic;

namespace Data.Configuration
{
    public class SignatureTypeConfiguartion : IEntityTypeConfiguration<SignatureType>
    {
        public void Configure(EntityTypeBuilder<SignatureType> builder)
        {
            builder.ToTable("signature_types");
            builder.Property(s => s.SignatureTypeName).HasMaxLength(400);
            List<SignatureType> SignatureTypes = new List<SignatureType>();

            SignatureTypes.Add(new SignatureType
            {
                SignatureTypeID = 1,
                SignatureTypeName = "Draw"

            });
            SignatureTypes.Add(new SignatureType
            {
                SignatureTypeID = 2,
                SignatureTypeName = "Upload"

            });

             SignatureTypes.Add(new SignatureType
            {
                SignatureTypeID = 3,
                SignatureTypeName = "Type"

            });

             SignatureTypes.Add(new SignatureType
            {
                SignatureTypeID = 4,
                SignatureTypeName = "Saved Signature"

            });

            builder.HasData(SignatureTypes.ToArray());
        }

    }

}


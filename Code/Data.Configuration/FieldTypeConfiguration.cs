
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System.Collections.Generic;

namespace Data.Configuration
{
    public class FieldTypeConfiguration : IEntityTypeConfiguration<FieldType>
    {

        public void Configure(EntityTypeBuilder<FieldType> builder)
        {
            builder.ToTable("field_types");
            builder.Property(f => f.FieldTypeName).HasMaxLength(255);
            List<FieldType> FieldTypes = new List<FieldType>();

            FieldTypes.Add(new FieldType
            {
                FieldTypeID = 1,
                FieldTypeName = "Signature"


            });
            FieldTypes.Add(new FieldType
            {
                FieldTypeID = 2,
                FieldTypeName = "SignatureButton"

            });
            FieldTypes.Add(new FieldType
            {
                FieldTypeID = 3,
                FieldTypeName = "Button"

            });
            builder.HasData(FieldTypes.ToArray());
        }
    }
}

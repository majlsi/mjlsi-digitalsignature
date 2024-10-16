
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;


namespace Data.Configuration
{
    public class DocumentFieldConfiguration : IEntityTypeConfiguration<DocumentField>
    {

        public void Configure(EntityTypeBuilder<DocumentField> builder)
        {
            builder.Property(a => a.DocumentFieldValue).HasColumnType("text");
            builder.Property(a => a.DocumentFieldComment).HasMaxLength(4000);
            builder.ToTable("document_fields");

        }
    }
}

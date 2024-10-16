
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;


namespace Data.Configuration
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {

        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.Property(a => a.OriginalDocumentUrl).HasMaxLength(1000);
            builder.ToTable("documents");

        }
    }
}

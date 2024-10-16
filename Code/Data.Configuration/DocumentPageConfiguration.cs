

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;


namespace Data.Configuration
{
    public class DocumentPageConfiguration : IEntityTypeConfiguration<DocumentPage>
    {

        public void Configure(EntityTypeBuilder<DocumentPage> builder)
        {
            builder.Property(a => a.DocumentPageUrl).HasMaxLength(1000);
            builder.ToTable("document_pages");

        }
    }
}

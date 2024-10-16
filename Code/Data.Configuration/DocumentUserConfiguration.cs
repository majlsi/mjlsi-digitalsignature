
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;


namespace Data.Configuration
{
    public class DocumentUserConfiguration : IEntityTypeConfiguration<DocumentUser>
    {

        public void Configure(EntityTypeBuilder<DocumentUser> builder)
        {
            builder.ToTable("document_users");

        }
    }
}

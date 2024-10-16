
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;


namespace Data.Configuration
{
    public class DocumentUserActionConfiguration : IEntityTypeConfiguration<DocumentUserAction>
    {

        public void Configure(EntityTypeBuilder<DocumentUserAction> builder)
        {
            builder.ToTable("document_user_actions");

        }
    }
}

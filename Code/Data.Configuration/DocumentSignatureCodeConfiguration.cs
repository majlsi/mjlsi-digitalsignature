using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
namespace Data.Configuration
{
	public class DocumentSignatureCodeConfiguration : IEntityTypeConfiguration<DocumentSignatureCode>
    {
        public void Configure(EntityTypeBuilder<DocumentSignatureCode> builder)
        {
            builder.Property(a => a.VerificationCode).HasMaxLength(100);
            builder.ToTable("document_signature_codes");
        }
    }
}

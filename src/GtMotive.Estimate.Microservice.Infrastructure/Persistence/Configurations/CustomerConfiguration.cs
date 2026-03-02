using GtMotive.Estimate.Microservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Configurations
{
    internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customers");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.FullName)
                .HasMaxLength(128)
                .IsRequired()
                .HasColumnName("full_name");

            builder.Property(c => c.CountryCode)
                .HasMaxLength(2)
                .IsRequired()
                .HasColumnName("country_code");

            builder.Property(c => c.DocumentType)
                .HasMaxLength(16)
                .IsRequired()
                .HasColumnName("document_type");

            builder.Property(c => c.DocumentNumber)
                .HasMaxLength(32)
                .IsRequired()
                .HasColumnName("document_number");

            builder.Property(c => c.Email)
                .HasMaxLength(256)
                .HasColumnName("email");

            builder.Property(c => c.Phone)
                .HasMaxLength(16)
                .HasColumnName("phone");

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired()
                .HasColumnName("is_deleted");

            builder.Property(c => c.CreatedAtUtc)
                .HasColumnName("created_at_utc");

            builder.Property(c => c.UpdatedAtUtc)
                .HasColumnName("updated_at_utc");

            builder.HasIndex(c => new { c.CountryCode, c.DocumentType, c.DocumentNumber })
                .IsUnique();

            builder.HasIndex(c => c.Email)
                .IsUnique()
                .HasFilter("\"email\" IS NOT NULL AND \"email\" <> ''");

            builder.HasIndex(c => c.IsDeleted);
        }
    }
}

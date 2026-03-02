using GtMotive.Estimate.Microservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// EF Core mapping for rental aggregate.
    /// </summary>
    internal sealed class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("rentals");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnName("id");

            builder.Property(r => r.VehicleId)
                .HasColumnName("vehicle_id");

            builder.Property(r => r.CustomerId)
                .IsRequired()
                .HasColumnName("customer_id");

            builder.Property(r => r.StartAtUtc)
                .HasColumnName("start_at_utc");

            builder.Property(r => r.EndAtUtc)
                .HasColumnName("end_at_utc");

            builder.Property(r => r.IsActive)
                .HasColumnName("is_active");

            builder.HasIndex(r => r.VehicleId);
            builder.HasIndex(r => r.CustomerId);
            builder.HasIndex(r => new { r.CustomerId, r.IsActive });

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

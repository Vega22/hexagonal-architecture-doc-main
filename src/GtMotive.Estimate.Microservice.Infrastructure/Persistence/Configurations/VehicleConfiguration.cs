using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// EF Core vehicle mapping.
    /// </summary>
    internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("vehicles");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasConversion(v => v.Value, v => new VehicleId(v))
                .ValueGeneratedNever()
                .HasColumnName("id");

            builder.Property(v => v.LicensePlate)
                .HasConversion(v => v.Value, v => new LicensePlate(v))
                .HasMaxLength(32)
                .IsRequired()
                .HasColumnName("license_plate");

            builder.HasIndex(v => v.LicensePlate)
                .IsUnique();

            builder.Property(v => v.ManufactureDate)
                .HasConversion(v => v.Value, v => new ManufactureDate(v))
                .IsRequired()
                .HasColumnName("manufacture_date");

            builder.Property(v => v.Brand)
                .HasMaxLength(64)
                .IsRequired()
                .HasColumnName("brand");

            builder.Property(v => v.Model)
                .HasMaxLength(64)
                .IsRequired()
                .HasColumnName("model");

            builder.Property(v => v.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            builder.Property(v => v.DeletedAtUtc)
                .HasColumnName("deleted_at_utc");

            builder.HasIndex(v => v.IsDeleted)
                .HasDatabaseName("IX_vehicles_is_deleted");
        }
    }
}

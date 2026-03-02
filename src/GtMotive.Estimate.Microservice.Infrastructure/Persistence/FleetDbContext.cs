using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence
{
    /// <summary>
    /// EF Core DbContext for fleet persistence.
    /// </summary>
    public sealed class FleetDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FleetDbContext"/> class.
        /// </summary>
        /// <param name="options">Db options.</param>
        public FleetDbContext(DbContextOptions<FleetDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets vehicles set.
        /// </summary>
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();

        /// <summary>
        /// Gets customers set.
        /// </summary>
        public DbSet<Customer> Customers => Set<Customer>();

        /// <summary>
        /// Gets rentals set.
        /// </summary>
        public DbSet<Rental> Rentals => Set<Rental>();

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new RentalConfiguration());
        }
    }
}

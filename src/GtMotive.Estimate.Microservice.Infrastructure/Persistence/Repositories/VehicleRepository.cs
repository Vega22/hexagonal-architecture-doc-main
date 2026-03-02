#nullable enable
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// EF Core implementation for vehicle and rental repository contracts.
    /// </summary>
    public sealed class VehicleRepository : IVehicleRepository, IRentalRepository
    {
        private readonly FleetDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRepository"/> class.
        /// </summary>
        /// <param name="context">Db context.</param>
        public VehicleRepository(FleetDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task Add(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
        }

        /// <inheritdoc />
        public async Task<Vehicle?> GetById(VehicleId vehicleId)
        {
            return await _context.Vehicles
                .SingleOrDefaultAsync(v => v.Id == vehicleId && !v.IsDeleted);
        }

        /// <inheritdoc />
        public async Task<Vehicle?> GetByIdIncludingDeleted(VehicleId vehicleId)
        {
            return await _context.Vehicles
                .SingleOrDefaultAsync(v => v.Id == vehicleId);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<Vehicle>> ListAvailable()
        {
            var activeVehicleIds = await _context.Rentals
                .Where(r => r.IsActive)
                .Select(r => r.VehicleId)
                .ToListAsync();

            var vehicles = await _context.Vehicles
                .Where(v => !v.IsDeleted)
                .OrderBy(v => v.LicensePlate)
                .ToListAsync();

            return vehicles
                .Where(v => !activeVehicleIds.Contains(v.Id.Value))
                .ToList();
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<Vehicle>> List(bool includeDeleted)
        {
            var query = _context.Vehicles.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(v => !v.IsDeleted);
            }

            return await query
                .OrderBy(v => v.LicensePlate)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<bool> ExistsByLicensePlate(LicensePlate licensePlate, Guid? excludedVehicleId = null)
        {
            var query = _context.Vehicles
                .Where(v => v.LicensePlate == licensePlate);

            if (excludedVehicleId.HasValue)
            {
                query = query.Where(v => v.Id != new VehicleId(excludedVehicleId.Value));
            }

            return await query.AnyAsync();
        }

        /// <inheritdoc />
        public async Task<bool> HasActiveRental(CustomerId customerId)
        {
            return await _context.Rentals
                .AnyAsync(r => r.CustomerId == customerId.Value && r.IsActive);
        }

        /// <inheritdoc />
        public async Task<bool> HasActiveRentalForVehicle(VehicleId vehicleId)
        {
            return await _context.Rentals
                .AnyAsync(r => r.VehicleId == vehicleId.Value && r.IsActive);
        }

        /// <inheritdoc />
        public async Task<bool> HasActiveRentalExcluding(CustomerId customerId, Guid rentalId)
        {
            return await _context.Rentals
                .AnyAsync(r => r.CustomerId == customerId.Value && r.IsActive && r.Id != rentalId);
        }

        /// <inheritdoc />
        public async Task<bool> HasActiveRentalForVehicleExcluding(VehicleId vehicleId, Guid rentalId)
        {
            return await _context.Rentals
                .AnyAsync(r => r.VehicleId == vehicleId.Value && r.IsActive && r.Id != rentalId);
        }

        /// <inheritdoc />
        public async Task Add(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
        }

        /// <inheritdoc />
        public async Task<Rental?> GetActiveByVehicle(VehicleId vehicleId)
        {
            return await _context.Rentals
                .Where(r => r.VehicleId == vehicleId.Value && r.IsActive)
                .OrderByDescending(r => r.StartAtUtc)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<Rental?> GetById(Guid rentalId)
        {
            return await _context.Rentals
                .SingleOrDefaultAsync(r => r.Id == rentalId);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<Rental>> List()
        {
            return await _context.Rentals
                .OrderByDescending(r => r.StartAtUtc)
                .ToListAsync();
        }

        /// <inheritdoc />
        public Task Remove(Rental rental)
        {
            _context.Rentals.Remove(rental);
            return Task.CompletedTask;
        }
    }
}

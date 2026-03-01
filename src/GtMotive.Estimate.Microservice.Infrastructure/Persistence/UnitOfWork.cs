using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence
{
    /// <summary>
    /// Unit of work implementation based on EF Core.
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly FleetDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">Db context.</param>
        public UnitOfWork(FleetDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

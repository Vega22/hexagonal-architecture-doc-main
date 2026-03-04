#nullable enable
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Events;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence
{
    /// <summary>
    /// Unit of work implementation based on EF Core.
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly FleetDbContext _context;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private IDbContextTransaction? _transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">Db context.</param>
        /// <param name="domainEventDispatcher">Domain event dispatcher.</param>
        public UnitOfWork(FleetDbContext context, IDomainEventDispatcher domainEventDispatcher)
        {
            _context = context;
            _domainEventDispatcher = domainEventDispatcher;
        }

        /// <inheritdoc />
        public async Task BeginTransaction()
        {
            if (_transaction is not null)
            {
                return;
            }

            if (_context.Database.ProviderName?.Contains("InMemory", StringComparison.OrdinalIgnoreCase) == true)
            {
                return;
            }

            _transaction = await _context.Database.BeginTransactionAsync();
        }

        /// <inheritdoc />
        public async Task CommitTransaction()
        {
            if (_transaction is null)
            {
                return;
            }

            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        /// <inheritdoc />
        public async Task RollbackTransaction()
        {
            if (_transaction is null)
            {
                return;
            }

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        /// <inheritdoc />
        public async Task<int> Save()
        {
            var domainEvents = _context.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(entry => entry.Entity)
                .SelectMany(aggregate => aggregate.DomainEvents)
                .ToList();

            var affectedRows = await _context.SaveChangesAsync();

            foreach (var aggregate in _context.ChangeTracker
                         .Entries<AggregateRoot>()
                         .Select(entry => entry.Entity))
            {
                aggregate.ClearDomainEvents();
            }

            if (domainEvents.Count > 0)
            {
                await _domainEventDispatcher.Dispatch(domainEvents);
            }

            return affectedRows;
        }
    }
}

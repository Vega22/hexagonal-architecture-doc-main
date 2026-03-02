#nullable enable
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Repositories
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly FleetDbContext _context;

        public CustomerRepository(FleetDbContext context)
        {
            _context = context;
        }

        public async Task Add(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task<Customer?> GetById(Guid customerId)
        {
            return await _context.Customers
                .SingleOrDefaultAsync(c => c.Id == customerId && !c.IsDeleted);
        }

        public async Task<Customer?> GetByIdIncludingDeleted(Guid customerId)
        {
            return await _context.Customers
                .SingleOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task<IReadOnlyCollection<Customer>> List(bool includeDeleted)
        {
            var query = _context.Customers.AsQueryable();
            if (!includeDeleted)
            {
                query = query.Where(c => !c.IsDeleted);
            }

            return await query
                .OrderBy(c => c.FullName)
                .ToListAsync();
        }

        public async Task<bool> ExistsByDocument(
            string countryCode,
            string documentType,
            string documentNumber,
            Guid? excludedCustomerId = null)
        {
            var query = _context.Customers
                .Where(c =>
                    c.CountryCode == countryCode &&
                    c.DocumentType == documentType &&
                    c.DocumentNumber == documentNumber);

            if (excludedCustomerId.HasValue)
            {
                query = query.Where(c => c.Id != excludedCustomerId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> ExistsByEmail(string email, Guid? excludedCustomerId = null)
        {
            var query = _context.Customers
                .Where(c => c.Email == email);

            if (excludedCustomerId.HasValue)
            {
                query = query.Where(c => c.Id != excludedCustomerId.Value);
            }

            return await query.AnyAsync();
        }
    }
}

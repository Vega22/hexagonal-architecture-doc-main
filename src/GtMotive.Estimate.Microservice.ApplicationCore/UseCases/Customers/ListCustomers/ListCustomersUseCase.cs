#nullable enable
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.ListCustomers
{
    public interface IListCustomersUseCase : IUseCase<ListCustomersInput>
    {
    }

    public interface IListCustomersOutputPort
    {
        void StandardHandle(ListCustomersOutput output);
    }

    public sealed class ListCustomersInput : IUseCaseInput
    {
        public ListCustomersInput(bool includeDeleted)
        {
            IncludeDeleted = includeDeleted;
        }

        public bool IncludeDeleted { get; }
    }

    public sealed class CustomerListItemOutput
    {
        public CustomerListItemOutput(
            Guid customerId,
            string fullName,
            string countryCode,
            string documentType,
            string documentNumber,
            string? email,
            string? phone,
            bool isDeleted,
            DateTime createdAtUtc,
            DateTime updatedAtUtc)
        {
            CustomerId = customerId;
            FullName = fullName;
            CountryCode = countryCode;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            Email = email;
            Phone = phone;
            IsDeleted = isDeleted;
            CreatedAtUtc = createdAtUtc;
            UpdatedAtUtc = updatedAtUtc;
        }

        public Guid CustomerId { get; }

        public string FullName { get; }

        public string CountryCode { get; }

        public string DocumentType { get; }

        public string DocumentNumber { get; }

        public string? Email { get; }

        public string? Phone { get; }

        public bool IsDeleted { get; }

        public DateTime CreatedAtUtc { get; }

        public DateTime UpdatedAtUtc { get; }
    }

    public sealed class ListCustomersOutput : IUseCaseOutput
    {
        public ListCustomersOutput(IReadOnlyCollection<CustomerListItemOutput> customers)
        {
            Customers = customers;
        }

        public IReadOnlyCollection<CustomerListItemOutput> Customers { get; }
    }

    public sealed class ListCustomersUseCase : IListCustomersUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IListCustomersOutputPort _outputPort;

        public ListCustomersUseCase(ICustomerRepository customerRepository, IListCustomersOutputPort outputPort)
        {
            _customerRepository = customerRepository;
            _outputPort = outputPort;
        }

        public async Task Execute(ListCustomersInput input)
        {
            var customers = await _customerRepository.List(input.IncludeDeleted);
            _outputPort.StandardHandle(
                new ListCustomersOutput(
                    customers
                        .Select(c => new CustomerListItemOutput(
                            c.Id,
                            c.FullName,
                            c.CountryCode,
                            c.DocumentType,
                            c.DocumentNumber,
                            c.Email,
                            c.Phone,
                            c.IsDeleted,
                            c.CreatedAtUtc,
                            c.UpdatedAtUtc))
                        .ToList()));
        }
    }
}

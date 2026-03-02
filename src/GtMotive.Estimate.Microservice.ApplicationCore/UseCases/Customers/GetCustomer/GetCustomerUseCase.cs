#nullable enable
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.GetCustomer
{
    public interface IGetCustomerUseCase : IUseCase<GetCustomerInput>
    {
    }

    public interface IGetCustomerOutputPort
    {
        void StandardHandle(GetCustomerOutput output);

        void NotFoundHandle(string message);
    }

    public sealed class GetCustomerInput : IUseCaseInput
    {
        public GetCustomerInput(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }

    public sealed class GetCustomerOutput : IUseCaseOutput
    {
        public GetCustomerOutput(
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

    public sealed class GetCustomerUseCase : IGetCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IGetCustomerOutputPort _outputPort;

        public GetCustomerUseCase(ICustomerRepository customerRepository, IGetCustomerOutputPort outputPort)
        {
            _customerRepository = customerRepository;
            _outputPort = outputPort;
        }

        public async Task Execute(GetCustomerInput input)
        {
            var customer = await _customerRepository.GetById(input.CustomerId);
            if (customer is null)
            {
                _outputPort.NotFoundHandle($"Customer '{input.CustomerId}' was not found.");
                return;
            }

            _outputPort.StandardHandle(
                new GetCustomerOutput(
                    customer.Id,
                    customer.FullName,
                    customer.CountryCode,
                    customer.DocumentType,
                    customer.DocumentNumber,
                    customer.Email,
                    customer.Phone,
                    customer.IsDeleted,
                    customer.CreatedAtUtc,
                    customer.UpdatedAtUtc));
        }
    }
}

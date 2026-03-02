#nullable enable
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.CreateCustomer
{
    public interface ICreateCustomerUseCase : IUseCase<CreateCustomerInput>
    {
    }

    public interface ICreateCustomerOutputPort
    {
        void StandardHandle(CreateCustomerOutput output);

        void ConflictHandle(string message);
    }

    public sealed class CreateCustomerInput : IUseCaseInput
    {
        public CreateCustomerInput(
            string fullName,
            string countryCode,
            string documentType,
            string documentNumber,
            string? email,
            string? phone)
        {
            FullName = fullName;
            CountryCode = countryCode;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            Email = email;
            Phone = phone;
        }

        public string FullName { get; }

        public string CountryCode { get; }

        public string DocumentType { get; }

        public string DocumentNumber { get; }

        public string? Email { get; }

        public string? Phone { get; }
    }

    public sealed class CreateCustomerOutput : IUseCaseOutput
    {
        public CreateCustomerOutput(
            Guid customerId,
            string fullName,
            string countryCode,
            string documentType,
            string documentNumber,
            string? email,
            string? phone,
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

        public DateTime CreatedAtUtc { get; }

        public DateTime UpdatedAtUtc { get; }
    }

    public sealed class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICreateCustomerOutputPort _outputPort;

        public CreateCustomerUseCase(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork,
            ICreateCustomerOutputPort outputPort)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        public async Task Execute(CreateCustomerInput input)
        {
            var customer = Customer.Create(
                input.FullName,
                input.CountryCode,
                input.DocumentType,
                input.DocumentNumber,
                input.Email,
                input.Phone);

            if (await _customerRepository.ExistsByDocument(
                    customer.CountryCode,
                    customer.DocumentType,
                    customer.DocumentNumber))
            {
                _outputPort.ConflictHandle($"Document number '{customer.DocumentNumber}' already exists.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(customer.Email) &&
                await _customerRepository.ExistsByEmail(customer.Email))
            {
                _outputPort.ConflictHandle($"Email '{customer.Email}' already exists.");
                return;
            }

            await _customerRepository.Add(customer);
            await _unitOfWork.Save();

            _outputPort.StandardHandle(
                new CreateCustomerOutput(
                    customer.Id,
                    customer.FullName,
                    customer.CountryCode,
                    customer.DocumentType,
                    customer.DocumentNumber,
                    customer.Email,
                    customer.Phone,
                    customer.CreatedAtUtc,
                    customer.UpdatedAtUtc));
        }
    }
}

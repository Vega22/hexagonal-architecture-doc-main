#nullable enable
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.UpdateCustomer
{
    public interface IUpdateCustomerUseCase : IUseCase<UpdateCustomerInput>
    {
    }

    public interface IUpdateCustomerOutputPort
    {
        void StandardHandle(UpdateCustomerOutput output);

        void NotFoundHandle(string message);

        void ConflictHandle(string message);
    }

    public sealed class UpdateCustomerInput : IUseCaseInput
    {
        public UpdateCustomerInput(
            Guid customerId,
            string fullName,
            string countryCode,
            string documentType,
            string documentNumber,
            string? email,
            string? phone)
        {
            CustomerId = customerId;
            FullName = fullName;
            CountryCode = countryCode;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            Email = email;
            Phone = phone;
        }

        public Guid CustomerId { get; }

        public string FullName { get; }

        public string CountryCode { get; }

        public string DocumentType { get; }

        public string DocumentNumber { get; }

        public string? Email { get; }

        public string? Phone { get; }
    }

    public sealed class UpdateCustomerOutput : IUseCaseOutput
    {
        public UpdateCustomerOutput(
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

    public sealed class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUpdateCustomerOutputPort _outputPort;

        public UpdateCustomerUseCase(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork,
            IUpdateCustomerOutputPort outputPort)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        public async Task Execute(UpdateCustomerInput input)
        {
            var customer = await _customerRepository.GetById(input.CustomerId);
            if (customer is null)
            {
                _outputPort.NotFoundHandle($"Customer '{input.CustomerId}' was not found.");
                return;
            }

            customer.Update(
                input.FullName,
                input.CountryCode,
                input.DocumentType,
                input.DocumentNumber,
                input.Email,
                input.Phone);

            if (await _customerRepository.ExistsByDocument(
                    customer.CountryCode,
                    customer.DocumentType,
                    customer.DocumentNumber,
                    customer.Id))
            {
                _outputPort.ConflictHandle($"Document number '{customer.DocumentNumber}' already exists.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(customer.Email) &&
                await _customerRepository.ExistsByEmail(customer.Email, customer.Id))
            {
                _outputPort.ConflictHandle($"Email '{customer.Email}' already exists.");
                return;
            }

            await _unitOfWork.Save();

            _outputPort.StandardHandle(
                new UpdateCustomerOutput(
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

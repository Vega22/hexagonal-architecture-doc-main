#nullable enable
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.CreateCustomer;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Customers
{
    public sealed class CreateCustomerRequest : IRequest<IWebApiPresenter>
    {
        public CreateCustomerRequest(
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

    public sealed class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, IWebApiPresenter>
    {
        private readonly ICreateCustomerUseCase _useCase;
        private readonly CreateCustomerPresenter _presenter;

        public CreateCustomerRequestHandler(ICreateCustomerUseCase useCase, CreateCustomerPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(
                new CreateCustomerInput(
                    request.FullName,
                    request.CountryCode,
                    request.DocumentType,
                    request.DocumentNumber,
                    request.Email,
                    request.Phone));
            return _presenter;
        }
    }
}

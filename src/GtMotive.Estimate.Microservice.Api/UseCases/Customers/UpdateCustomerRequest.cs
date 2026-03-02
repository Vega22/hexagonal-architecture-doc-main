#nullable enable
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.UpdateCustomer;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Customers
{
    public sealed class UpdateCustomerRequest : IRequest<IWebApiPresenter>
    {
        public UpdateCustomerRequest(
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

    public sealed class UpdateCustomerRequestHandler : IRequestHandler<UpdateCustomerRequest, IWebApiPresenter>
    {
        private readonly IUpdateCustomerUseCase _useCase;
        private readonly UpdateCustomerPresenter _presenter;

        public UpdateCustomerRequestHandler(IUpdateCustomerUseCase useCase, UpdateCustomerPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(new UpdateCustomerInput(
                request.CustomerId,
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

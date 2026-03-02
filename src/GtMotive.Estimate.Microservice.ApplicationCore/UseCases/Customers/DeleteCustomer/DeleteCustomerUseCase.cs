using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.DeleteCustomer
{
    public interface IDeleteCustomerUseCase : IUseCase<DeleteCustomerInput>
    {
    }

    public interface IDeleteCustomerOutputPort
    {
        void StandardHandle();

        void NotFoundHandle(string message);

        void ConflictHandle(string message);
    }

    public sealed class DeleteCustomerInput : IUseCaseInput
    {
        public DeleteCustomerInput(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }

    public sealed class DeleteCustomerUseCase : IDeleteCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeleteCustomerOutputPort _outputPort;

        public DeleteCustomerUseCase(
            ICustomerRepository customerRepository,
            IRentalRepository rentalRepository,
            IUnitOfWork unitOfWork,
            IDeleteCustomerOutputPort outputPort)
        {
            _customerRepository = customerRepository;
            _rentalRepository = rentalRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        public async Task Execute(DeleteCustomerInput input)
        {
            var customer = await _customerRepository.GetById(input.CustomerId);
            if (customer is null)
            {
                _outputPort.NotFoundHandle($"Customer '{input.CustomerId}' was not found.");
                return;
            }

            if (await _rentalRepository.HasActiveRental(new CustomerId(input.CustomerId)))
            {
                _outputPort.ConflictHandle($"Customer '{input.CustomerId}' has an active rental.");
                return;
            }

            customer.MarkDeleted();
            await _unitOfWork.Save();
            _outputPort.StandardHandle();
        }
    }
}

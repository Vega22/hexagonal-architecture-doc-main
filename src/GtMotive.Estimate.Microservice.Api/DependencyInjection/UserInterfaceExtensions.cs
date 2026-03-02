using GtMotive.Estimate.Microservice.Api.UseCases.Customers;
using GtMotive.Estimate.Microservice.Api.UseCases.Rentals;
using GtMotive.Estimate.Microservice.Api.UseCases.Vehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.CreateCustomer;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.DeleteCustomer;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.GetCustomer;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.ListCustomers;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.UpdateCustomer;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.ReturnVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.DeleteRental;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.GetRental;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.ListRentals;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.UpdateRental;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.DeleteVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.GetVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.UpdateVehicle;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Api.DependencyInjection
{
    public static class UserInterfaceExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<CreateVehiclePresenter>();
            services.AddScoped<CreateCustomerPresenter>();
            services.AddScoped<GetCustomerPresenter>();
            services.AddScoped<ListCustomersPresenter>();
            services.AddScoped<UpdateCustomerPresenter>();
            services.AddScoped<DeleteCustomerPresenter>();
            services.AddScoped<ListAvailableVehiclesPresenter>();
            services.AddScoped<GetVehiclePresenter>();
            services.AddScoped<ListVehiclesPresenter>();
            services.AddScoped<UpdateVehiclePresenter>();
            services.AddScoped<DeleteVehiclePresenter>();
            services.AddScoped<RentVehiclePresenter>();
            services.AddScoped<ReturnVehiclePresenter>();
            services.AddScoped<GetRentalPresenter>();
            services.AddScoped<ListRentalsPresenter>();
            services.AddScoped<UpdateRentalPresenter>();
            services.AddScoped<DeleteRentalPresenter>();

            services.AddScoped<ICreateVehicleOutputPort>(provider => provider.GetRequiredService<CreateVehiclePresenter>());
            services.AddScoped<ICreateCustomerOutputPort>(provider => provider.GetRequiredService<CreateCustomerPresenter>());
            services.AddScoped<IGetCustomerOutputPort>(provider => provider.GetRequiredService<GetCustomerPresenter>());
            services.AddScoped<IListCustomersOutputPort>(provider => provider.GetRequiredService<ListCustomersPresenter>());
            services.AddScoped<IUpdateCustomerOutputPort>(provider => provider.GetRequiredService<UpdateCustomerPresenter>());
            services.AddScoped<IDeleteCustomerOutputPort>(provider => provider.GetRequiredService<DeleteCustomerPresenter>());
            services.AddScoped<IListAvailableVehiclesOutputPort>(provider => provider.GetRequiredService<ListAvailableVehiclesPresenter>());
            services.AddScoped<IGetVehicleOutputPort>(provider => provider.GetRequiredService<GetVehiclePresenter>());
            services.AddScoped<IListVehiclesOutputPort>(provider => provider.GetRequiredService<ListVehiclesPresenter>());
            services.AddScoped<IUpdateVehicleOutputPort>(provider => provider.GetRequiredService<UpdateVehiclePresenter>());
            services.AddScoped<IDeleteVehicleOutputPort>(provider => provider.GetRequiredService<DeleteVehiclePresenter>());
            services.AddScoped<IRentVehicleOutputPort>(provider => provider.GetRequiredService<RentVehiclePresenter>());
            services.AddScoped<IReturnVehicleOutputPort>(provider => provider.GetRequiredService<ReturnVehiclePresenter>());
            services.AddScoped<IGetRentalOutputPort>(provider => provider.GetRequiredService<GetRentalPresenter>());
            services.AddScoped<IListRentalsOutputPort>(provider => provider.GetRequiredService<ListRentalsPresenter>());
            services.AddScoped<IUpdateRentalOutputPort>(provider => provider.GetRequiredService<UpdateRentalPresenter>());
            services.AddScoped<IDeleteRentalOutputPort>(provider => provider.GetRequiredService<DeleteRentalPresenter>());

            return services;
        }
    }
}

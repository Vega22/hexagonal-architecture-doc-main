using System;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.DeleteRental;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.GetRental;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.ListRentals;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.ReturnVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.UpdateRental;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.DeleteVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.GetVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.UpdateVehicle;
using Microsoft.Extensions.DependencyInjection;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.ApplicationCore
{
    /// <summary>
    /// Adds Use Cases classes.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// Adds Use Cases to the ServiceCollection.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>The modified instance.</returns>
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateVehicleUseCase, CreateVehicleUseCase>();
            services.AddScoped<IListAvailableVehiclesUseCase, ListAvailableVehiclesUseCase>();
            services.AddScoped<IGetVehicleUseCase, GetVehicleUseCase>();
            services.AddScoped<IListVehiclesUseCase, ListVehiclesUseCase>();
            services.AddScoped<IUpdateVehicleUseCase, UpdateVehicleUseCase>();
            services.AddScoped<IDeleteVehicleUseCase, DeleteVehicleUseCase>();

            services.AddScoped<IRentVehicleUseCase, RentVehicleUseCase>();
            services.AddScoped<IReturnVehicleUseCase, ReturnVehicleUseCase>();
            services.AddScoped<IGetRentalUseCase, GetRentalUseCase>();
            services.AddScoped<IListRentalsUseCase, ListRentalsUseCase>();
            services.AddScoped<IUpdateRentalUseCase, UpdateRentalUseCase>();
            services.AddScoped<IDeleteRentalUseCase, DeleteRentalUseCase>();

            return services;
        }
    }
}

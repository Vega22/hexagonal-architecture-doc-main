using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.Api.UseCases.Vehicles;
using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Specs
{
    /// <summary>
    /// Functional tests for vehicle use-case flow.
    /// </summary>
    public sealed class VehiclesFlowSpecs : FunctionalTestBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesFlowSpecs"/> class.
        /// </summary>
        /// <param name="fixture">Composition fixture.</param>
        public VehiclesFlowSpecs(CompositionRootTestFixture fixture)
            : base(fixture)
        {
        }

        /// <summary>
        /// Should create and then list an available vehicle.
        /// </summary>
        [Fact]
        public async Task CreateThenListAvailableVehicle_ShouldReturnCreatedVehicle()
        {
            await Fixture.UsingHandlerForRequestResponse<CreateVehicleRequest, IWebApiPresenter>(
                async handler =>
                {
                    var presenter = await handler.Handle(
                        new CreateVehicleRequest(
                            "1234XYZ",
                            DateOnly.FromDateTime(DateTime.UtcNow),
                            "Toyota",
                            "Corolla"),
                        default);

                    Assert.IsType<CreatedResult>(presenter.ActionResult);
                });

            await Fixture.UsingHandlerForRequestResponse<ListAvailableVehiclesRequest, IWebApiPresenter>(
                async handler =>
                {
                    var presenter = await handler.Handle(new ListAvailableVehiclesRequest(), default);
                    var ok = Assert.IsType<OkObjectResult>(presenter.ActionResult);
                    var vehicles = Assert.IsAssignableFrom<List<AvailableVehicleModel>>(ok.Value);
                    Assert.NotEmpty(vehicles);
                });
        }

        /// <summary>
        /// Should hide soft deleted vehicles from default list.
        /// </summary>
        [Fact]
        public async Task DeleteVehicle_ShouldNotBeReturnedInDefaultList()
        {
            Guid createdVehicleId = Guid.Empty;

            await Fixture.UsingHandlerForRequestResponse<CreateVehicleRequest, IWebApiPresenter>(
                async handler =>
                {
                    var presenter = await handler.Handle(
                        new CreateVehicleRequest(
                            "1235XYZ",
                            DateOnly.FromDateTime(DateTime.UtcNow),
                            "Hyundai",
                            "i20"),
                        default);

                    var created = Assert.IsType<CreatedResult>(presenter.ActionResult);
                    var body = Assert.IsType<CreateVehicleResponseModel>(created.Value);
                    createdVehicleId = body.VehicleId;
                });

            await Fixture.UsingHandlerForRequestResponse<DeleteVehicleRequest, IWebApiPresenter>(
                async handler =>
                {
                    var presenter = await handler.Handle(new DeleteVehicleRequest(createdVehicleId), default);
                    Assert.IsType<NoContentResult>(presenter.ActionResult);
                });

            await Fixture.UsingHandlerForRequestResponse<ListVehiclesRequest, IWebApiPresenter>(
                async handler =>
                {
                    var presenter = await handler.Handle(new ListVehiclesRequest(includeDeleted: false), default);
                    var ok = Assert.IsType<OkObjectResult>(presenter.ActionResult);
                    var vehicles = Assert.IsAssignableFrom<List<VehicleModel>>(ok.Value);
                    Assert.DoesNotContain(vehicles, v => v.VehicleId == createdVehicleId);
                });
        }
    }
}

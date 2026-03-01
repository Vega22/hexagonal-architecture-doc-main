using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RestructureVehiclesForRentalSourceOfTruth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "brand",
                table: "vehicles",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "Unknown");

            migrationBuilder.AddColumn<string>(
                name: "model",
                table: "vehicles",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "Unknown");

            migrationBuilder.DropColumn(
                name: "current_customer_id",
                table: "vehicles");

            migrationBuilder.DropColumn(
                name: "rented_at_utc",
                table: "vehicles");

            migrationBuilder.DropColumn(
                name: "status",
                table: "vehicles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "brand",
                table: "vehicles");

            migrationBuilder.DropColumn(
                name: "model",
                table: "vehicles");

            migrationBuilder.AddColumn<string>(
                name: "current_customer_id",
                table: "vehicles",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "rented_at_utc",
                table: "vehicles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "vehicles",
                type: "integer",
                nullable: false,
                defaultValue: 1);
        }
    }
}

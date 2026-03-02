using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerSpanishDocumentMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_customers_document_number",
                table: "customers");

            migrationBuilder.AddColumn<string>(
                name: "country_code",
                table: "customers",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "ES");

            migrationBuilder.AddColumn<string>(
                name: "document_type",
                table: "customers",
                type: "character varying(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "DNI");

            migrationBuilder.CreateIndex(
                name: "IX_customers_country_code_document_type_document_number",
                table: "customers",
                columns: new[] { "country_code", "document_type", "document_number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_customers_country_code_document_type_document_number",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "country_code",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "document_type",
                table: "customers");

            migrationBuilder.CreateIndex(
                name: "IX_customers_document_number",
                table: "customers",
                column: "document_number",
                unique: true);
        }
    }
}

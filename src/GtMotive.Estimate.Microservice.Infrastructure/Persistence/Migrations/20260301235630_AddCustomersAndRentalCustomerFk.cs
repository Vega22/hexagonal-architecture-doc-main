using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomersAndRentalCustomerFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    document_number = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    phone = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customers_document_number",
                table: "customers",
                column: "document_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_email",
                table: "customers",
                column: "email",
                unique: true,
                filter: "\"email\" IS NOT NULL AND \"email\" <> ''");

            migrationBuilder.CreateIndex(
                name: "IX_customers_is_deleted",
                table: "customers",
                column: "is_deleted");

            migrationBuilder.AddColumn<Guid>(
                name: "customer_id_v2",
                table: "rentals",
                type: "uuid",
                nullable: true);

            migrationBuilder.Sql(
                """
                UPDATE rentals
                SET customer_id_v2 = CASE
                    WHEN customer_id ~* '^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$'
                    THEN customer_id::uuid
                    ELSE NULL
                END;
                """);

            migrationBuilder.Sql(
                """
                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1
                        FROM rentals
                        WHERE customer_id_v2 IS NULL
                    ) THEN
                        RAISE EXCEPTION 'Migration blocked: rentals.customer_id contains non-GUID values. Fix legacy data first.';
                    END IF;
                END$$;
                """);

            migrationBuilder.Sql(
                """
                INSERT INTO customers (id, full_name, document_number, email, phone, is_deleted, created_at_utc, updated_at_utc)
                SELECT DISTINCT
                    customer_id_v2,
                    'Legacy Customer ' || LEFT(customer_id_v2::text, 8),
                    'LEG-' || LEFT(UPPER(REPLACE(customer_id_v2::text, '-', '')), 28),
                    NULL,
                    NULL,
                    FALSE,
                    NOW(),
                    NOW()
                FROM rentals r
                WHERE NOT EXISTS (
                    SELECT 1 FROM customers c WHERE c.id = r.customer_id_v2
                );
                """);

            migrationBuilder.DropColumn(
                name: "customer_id",
                table: "rentals");

            migrationBuilder.RenameColumn(
                name: "customer_id_v2",
                table: "rentals",
                newName: "customer_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "customer_id",
                table: "rentals",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_rentals_customers_customer_id",
                table: "rentals",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rentals_customers_customer_id",
                table: "rentals");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.AlterColumn<string>(
                name: "customer_id",
                table: "rentals",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}

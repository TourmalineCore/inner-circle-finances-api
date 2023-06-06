using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class AddNewEmployeeFinancialMetricsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeFinanceForPayroll");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeFinancialMetrics",
                table: "EmployeeFinancialMetrics");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeFinancialMetrics_EmployeeId",
                table: "EmployeeFinancialMetrics");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EmployeeFinancialMetrics");

            migrationBuilder.RenameColumn(
                name: "ActualFromUtc",
                table: "TotalFinances",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ActualFromUtc",
                table: "EmployeeFinancialMetrics",
                newName: "CreatedAtUtc");

            migrationBuilder.AddColumn<Instant>(
                name: "CreatedAtUtc",
                table: "EstimatedFinancialEfficiency",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<bool>(
                name: "IsEmployedOfficially",
                table: "EmployeeFinancialMetrics",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeFinancialMetrics",
                table: "EmployeeFinancialMetrics",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeFinancialMetrics",
                table: "EmployeeFinancialMetrics");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "EstimatedFinancialEfficiency");

            migrationBuilder.DropColumn(
                name: "IsEmployedOfficially",
                table: "EmployeeFinancialMetrics");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "TotalFinances",
                newName: "ActualFromUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "EmployeeFinancialMetrics",
                newName: "ActualFromUtc");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "EmployeeFinancialMetrics",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeFinancialMetrics",
                table: "EmployeeFinancialMetrics",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EmployeeFinanceForPayroll",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    EmploymentType = table.Column<decimal>(type: "numeric", nullable: false),
                    ParkingCostPerMonth = table.Column<decimal>(type: "numeric", nullable: false),
                    Pay = table.Column<decimal>(type: "numeric", nullable: false),
                    RatePerHour = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFinanceForPayroll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeFinanceForPayroll_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFinancialMetrics_EmployeeId",
                table: "EmployeeFinancialMetrics",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFinanceForPayroll_EmployeeId",
                table: "EmployeeFinanceForPayroll",
                column: "EmployeeId",
                unique: true);
        }
    }
}

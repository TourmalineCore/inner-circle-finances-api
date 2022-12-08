using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class MoveCoeffsAndTotalsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAtUtc",
                table: "EmployeeFinancialMetricsHistory",
                newName: "ToUtc");

            migrationBuilder.RenameColumn(
                name: "StartedAtUtc",
                table: "EmployeeFinancialMetricsHistory",
                newName: "FromUtc");

            migrationBuilder.CreateTable(
                name: "CoefficientOptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DistrictCoefficient = table.Column<double>(type: "double precision", nullable: false),
                    MinimumWage = table.Column<double>(type: "double precision", nullable: false),
                    IncomeTaxPercent = table.Column<double>(type: "double precision", nullable: false),
                    OfficeExpenses = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoefficientOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TotalFinances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActualFromUtc = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    PayrollExpense = table.Column<double>(type: "double precision", nullable: false),
                    OfficeExpense = table.Column<double>(type: "double precision", nullable: false),
                    TotalExpense = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalFinances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TotalFinancesHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromUtc = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    ToUtc = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    PayrollExpense = table.Column<double>(type: "double precision", nullable: false),
                    OfficeExpense = table.Column<double>(type: "double precision", nullable: false),
                    TotalExpense = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalFinancesHistory", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CoefficientOptions",
                columns: new[] { "Id", "DistrictCoefficient", "IncomeTaxPercent", "MinimumWage", "OfficeExpenses" },
                values: new object[] { 1L, 0.14999999999999999, 0.13, 15279.0, 49000.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoefficientOptions");

            migrationBuilder.DropTable(
                name: "TotalFinances");

            migrationBuilder.DropTable(
                name: "TotalFinancesHistory");

            migrationBuilder.RenameColumn(
                name: "ToUtc",
                table: "EmployeeFinancialMetricsHistory",
                newName: "UpdatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "FromUtc",
                table: "EmployeeFinancialMetricsHistory",
                newName: "StartedAtUtc");
        }
    }
}

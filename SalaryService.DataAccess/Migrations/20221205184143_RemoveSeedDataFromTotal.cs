using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class RemoveSeedDataFromTotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DesiredFinancesAndReserve",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "TotalFinances",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.RenameColumn(
                name: "DesiredIncome",
                table: "DesiredFinancesAndReserve",
                newName: "DesiredEarnings");

            migrationBuilder.AddColumn<double>(
                name: "DistrictCoefficient",
                table: "EmployeeFinancialMetrics",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictCoefficient",
                table: "EmployeeFinancialMetrics");

            migrationBuilder.RenameColumn(
                name: "DesiredEarnings",
                table: "DesiredFinancesAndReserve",
                newName: "DesiredIncome");

            migrationBuilder.InsertData(
                table: "DesiredFinancesAndReserve",
                columns: new[] { "Id", "DesiredIncome", "DesiredProfit", "DesiredProfitability", "ReserveForHalfYear", "ReserveForQuarter", "ReserveForYear" },
                values: new object[] { 1L, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });

            migrationBuilder.InsertData(
                table: "TotalFinances",
                columns: new[] { "Id", "ActualFromUtc", "PayrollExpense", "TotalExpense" },
                values: new object[] { 1L, NodaTime.Instant.FromUnixTimeTicks(0L), 0.0, 0.0 });
        }
    }
}

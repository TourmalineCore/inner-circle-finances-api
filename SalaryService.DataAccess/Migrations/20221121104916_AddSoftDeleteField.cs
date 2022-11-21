using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class AddSoftDeleteField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Instant>(
                name: "DeletedAtUtc",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Instant>(
                name: "DeletedAtUtc",
                table: "EmployeeFinancialMetricsHistory",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Instant>(
                name: "DeletedAtUtc",
                table: "EmployeeFinancialMetrics",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Instant>(
                name: "DeletedAtUtc",
                table: "EmployeeFinanceForPayroll",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "EmployeeFinancialMetricsHistory");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "EmployeeFinancialMetrics");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "EmployeeFinanceForPayroll");
        }
    }
}

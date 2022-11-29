using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class RenameRetainerRemoveHasParking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasParking",
                table: "EmployeeFinancialMetricsHistory");

            migrationBuilder.DropColumn(
                name: "HasParking",
                table: "EmployeeFinancialMetrics");

            migrationBuilder.DropColumn(
                name: "HasParking",
                table: "EmployeeFinanceForPayroll");

            migrationBuilder.RenameColumn(
                name: "Retainer",
                table: "EmployeeFinancialMetricsHistory",
                newName: "Prepayment");

            migrationBuilder.RenameColumn(
                name: "Retainer",
                table: "EmployeeFinancialMetrics",
                newName: "Prepayment");

            migrationBuilder.AddColumn<double>(
                name: "ParkingCostPerMonth",
                table: "EmployeeFinanceForPayroll",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingCostPerMonth",
                table: "EmployeeFinanceForPayroll");

            migrationBuilder.RenameColumn(
                name: "Prepayment",
                table: "EmployeeFinancialMetricsHistory",
                newName: "Retainer");

            migrationBuilder.RenameColumn(
                name: "Prepayment",
                table: "EmployeeFinancialMetrics",
                newName: "Retainer");

            migrationBuilder.AddColumn<bool>(
                name: "HasParking",
                table: "EmployeeFinancialMetricsHistory",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasParking",
                table: "EmployeeFinancialMetrics",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasParking",
                table: "EmployeeFinanceForPayroll",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

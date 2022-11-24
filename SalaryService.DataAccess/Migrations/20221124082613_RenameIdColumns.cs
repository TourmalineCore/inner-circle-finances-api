using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class RenameIdColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FinancialMetricsId",
                table: "Employees",
                newName: "EmployeeFinancialMetricsId");

            migrationBuilder.RenameColumn(
                name: "FinanceForPayrollId",
                table: "Employees",
                newName: "EmployeeFinanceForPayrollId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeFinancialMetricsId",
                table: "Employees",
                newName: "FinancialMetricsId");

            migrationBuilder.RenameColumn(
                name: "EmployeeFinanceForPayrollId",
                table: "Employees",
                newName: "FinanceForPayrollId");
        }
    }
}

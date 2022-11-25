using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class AddForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeFinanceForPayrollId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeFinancialMetricsId",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EmployeeFinanceForPayrollId",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeFinancialMetricsId",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}

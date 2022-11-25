using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class AddedMetricsAndFinanceForPayroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeFinancialMetrics_EmployeeId",
                table: "EmployeeFinancialMetrics");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeFinanceForPayroll_EmployeeId",
                table: "EmployeeFinanceForPayroll");

            migrationBuilder.AddColumn<long>(
                name: "FinanceForPayrollId",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FinancialMetricsId",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeFinancialMetrics_EmployeeId",
                table: "EmployeeFinancialMetrics");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeFinanceForPayroll_EmployeeId",
                table: "EmployeeFinanceForPayroll");

            migrationBuilder.DropColumn(
                name: "FinanceForPayrollId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FinancialMetricsId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFinancialMetrics_EmployeeId",
                table: "EmployeeFinancialMetrics",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFinanceForPayroll_EmployeeId",
                table: "EmployeeFinanceForPayroll",
                column: "EmployeeId");
        }
    }
}

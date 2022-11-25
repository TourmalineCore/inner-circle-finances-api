using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class AddedForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFinancialMetricsHistory_EmployeeId",
                table: "EmployeeFinancialMetricsHistory",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFinancialMetrics_EmployeeId",
                table: "EmployeeFinancialMetrics",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeFinancialMetrics_Employees_EmployeeId",
                table: "EmployeeFinancialMetrics",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeFinancialMetricsHistory_Employees_EmployeeId",
                table: "EmployeeFinancialMetricsHistory",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeFinancialMetrics_Employees_EmployeeId",
                table: "EmployeeFinancialMetrics");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeFinancialMetricsHistory_Employees_EmployeeId",
                table: "EmployeeFinancialMetricsHistory");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeFinancialMetricsHistory_EmployeeId",
                table: "EmployeeFinancialMetricsHistory");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeFinancialMetrics_EmployeeId",
                table: "EmployeeFinancialMetrics");
        }
    }
}

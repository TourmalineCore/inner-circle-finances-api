using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class AddDisctrictCoefficient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DistrictContributions",
                table: "EmployeeFinancialMetrics",
                newName: "DistrictCoefficient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DistrictCoefficient",
                table: "EmployeeFinancialMetrics",
                newName: "DistrictContributions");
        }
    }
}

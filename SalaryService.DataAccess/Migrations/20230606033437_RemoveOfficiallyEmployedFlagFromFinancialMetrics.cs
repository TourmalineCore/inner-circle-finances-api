using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class RemoveOfficiallyEmployedFlagFromFinancialMetrics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmployedOfficially",
                table: "EmployeeFinancialMetrics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmployedOfficially",
                table: "EmployeeFinancialMetrics",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

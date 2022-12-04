using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class SaveOfficeExpensesInCoefTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfficeExpense",
                table: "TotalFinances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OfficeExpense",
                table: "TotalFinances",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

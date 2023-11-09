using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class RenameDateOfCompensations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Compensations",
                newName: "DateCreateCompensation");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Compensations",
                newName: "DateCompensation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreateCompensation",
                table: "Compensations",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "DateCompensation",
                table: "Compensations",
                newName: "CreatedAtUtc");
        }
    }
}

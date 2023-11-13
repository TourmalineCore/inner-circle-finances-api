using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class ChangeCompensationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Compensations");

            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                table: "Compensations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Compensations");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Compensations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

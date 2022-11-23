using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class ChangeFieldsToPofile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telegram",
                table: "Employees",
                newName: "GitLab");

            migrationBuilder.RenameColumn(
                name: "Skype",
                table: "Employees",
                newName: "GitHub");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_Telegram",
                table: "Employees",
                newName: "IX_Employees_GitLab");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_Skype",
                table: "Employees",
                newName: "IX_Employees_GitHub");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GitLab",
                table: "Employees",
                newName: "Telegram");

            migrationBuilder.RenameColumn(
                name: "GitHub",
                table: "Employees",
                newName: "Skype");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_GitLab",
                table: "Employees",
                newName: "IX_Employees_Telegram");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_GitHub",
                table: "Employees",
                newName: "IX_Employees_Skype");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class AddedValidationForUniqueFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WorkEmail",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PersonalEmail",
                table: "Employees",
                column: "PersonalEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Phone",
                table: "Employees",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Skype",
                table: "Employees",
                column: "Skype",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Telegram",
                table: "Employees",
                column: "Telegram",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WorkEmail",
                table: "Employees",
                column: "WorkEmail",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_PersonalEmail",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Phone",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Skype",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Telegram",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_WorkEmail",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "WorkEmail",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}

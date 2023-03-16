using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class AddTypeOfEmployment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_GitHub",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_GitLab",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Phone",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCurrentEmployee",
                table: "Employees",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmployedOfficially",
                table: "Employees",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PersonnelNumber",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 50L,
                columns: new[] { "IsBlankEmployee", "IsEmployedOfficially" },
                values: new object[] { true, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmployedOfficially",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PersonnelNumber",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCurrentEmployee",
                table: "Employees",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 50L,
                column: "IsBlankEmployee",
                value: false);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GitHub",
                table: "Employees",
                column: "GitHub",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GitLab",
                table: "Employees",
                column: "GitLab",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Phone",
                table: "Employees",
                column: "Phone",
                unique: true);
        }
    }
}

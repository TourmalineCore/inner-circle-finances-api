using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class AddFlagForSpecialEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecial",
                table: "Employees",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CorporateEmail", "DeletedAtUtc", "FirstName", "GitHub", "GitLab", "HireDate", "IsBlankEmployee", "IsCurrentEmployee", "IsEmployedOfficially", "IsSpecial", "LastName", "MiddleName", "PersonalEmail", "PersonnelNumber", "Phone" },
                values: new object[,]
                {
                    { 1L, "ceo@tourmalinecore.com", null, "Ceo", "@ceo.github", "@ceo.gitlab", NodaTime.Instant.FromUnixTimeTicks(15778368000000000L), true, true, true, false, "Ceo", "Ceo", "ceo@gmail.com", null, "88006663636" },
                    { 2L, "inner-circle-admin@tourmalinecore.com", null, "Admin", null, null, NodaTime.Instant.FromUnixTimeTicks(15778368000000000L), true, true, true, true, "Admin", "Admin", "inner-circle-admin@gmail.com", null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DropColumn(
                name: "IsSpecial",
                table: "Employees");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CorporateEmail", "DeletedAtUtc", "FirstName", "GitHub", "GitLab", "HireDate", "IsBlankEmployee", "IsCurrentEmployee", "IsEmployedOfficially", "LastName", "MiddleName", "PersonalEmail", "PersonnelNumber", "Phone" },
                values: new object[] { 50L, "ceo@tourmalinecore.com", null, "Ceo", "@ceo.github", "@ceo.gitlab", NodaTime.Instant.FromUnixTimeTicks(15778368000000000L), true, true, true, "Ceo", "Ceo", "ceo@gmail.com", null, "88006663636" });
        }
    }
}

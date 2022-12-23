using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class AddDefaultCeo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AccountId", "CorporateEmail", "DeletedAtUtc", "GitHub", "GitLab", "HireDate", "MiddleName", "Name", "PersonalEmail", "Phone", "Surname" },
                values: new object[] { 50L, 1L, "ceo@tourmalinecore.com", null, "@ceo.github", "@ceo.gitlab", NodaTime.Instant.FromUnixTimeTicks(0L), "Ceo", "Ceo", "ceo@gmail.com", "88006663636", "Ceo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 50L);
        }
    }
}

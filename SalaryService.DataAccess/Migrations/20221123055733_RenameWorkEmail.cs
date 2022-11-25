using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class RenameWorkEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkEmail",
                table: "Employees",
                newName: "CorporateEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_WorkEmail",
                table: "Employees",
                newName: "IX_Employees_CorporateEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorporateEmail",
                table: "Employees",
                newName: "WorkEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CorporateEmail",
                table: "Employees",
                newName: "IX_Employees_WorkEmail");
        }
    }
}

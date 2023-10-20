using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class AddedEmployeeToCompensations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "Compensations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Compensations_EmployeeId",
                table: "Compensations",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compensations_Employees_EmployeeId",
                table: "Compensations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compensations_Employees_EmployeeId",
                table: "Compensations");

            migrationBuilder.DropIndex(
                name: "IX_Compensations_EmployeeId",
                table: "Compensations");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Compensations");
        }
    }
}

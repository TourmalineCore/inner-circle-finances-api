using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class ChangeWorkingPlane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WorkingPlan",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "WorkingDaysInMonth", "WorkingHoursInMonth" },
                values: new object[] { 16.916666666666666666666666667m, 135.33333333333333333333333334m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WorkingPlan",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "WorkingDaysInMonth", "WorkingHoursInMonth" },
                values: new object[] { 16.91666667m, 135.3333333m });
        }
    }
}

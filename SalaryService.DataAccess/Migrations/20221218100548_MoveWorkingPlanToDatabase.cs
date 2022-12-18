using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class MoveWorkingPlanToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkingPlan",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkingDaysInYear = table.Column<double>(type: "double precision", nullable: false),
                    WorkingDaysInYearWithoutVacation = table.Column<double>(type: "double precision", nullable: false),
                    WorkingDaysInYearWithoutVacationAndSick = table.Column<double>(type: "double precision", nullable: false),
                    WorkingDaysInMonth = table.Column<double>(type: "double precision", nullable: false),
                    WorkingHoursInMonth = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPlan", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WorkingPlan",
                columns: new[] { "Id", "WorkingDaysInMonth", "WorkingDaysInYear", "WorkingDaysInYearWithoutVacation", "WorkingDaysInYearWithoutVacationAndSick", "WorkingHoursInMonth" },
                values: new object[] { 1L, 16.916666670000001, 247.0, 223.0, 203.0, 135.33333329999999 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingPlan");
        }
    }
}

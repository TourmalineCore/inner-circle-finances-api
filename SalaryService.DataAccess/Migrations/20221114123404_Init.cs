using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeFinancialMetrics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    StartedAtUtc = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    Salary = table.Column<double>(type: "double precision", nullable: false),
                    HourlyCostFact = table.Column<double>(type: "double precision", nullable: false),
                    HourlyCostHand = table.Column<double>(type: "double precision", nullable: false),
                    Earnings = table.Column<double>(type: "double precision", nullable: false),
                    Expenses = table.Column<double>(type: "double precision", nullable: false),
                    Profit = table.Column<double>(type: "double precision", nullable: false),
                    ProfitAbility = table.Column<double>(type: "double precision", nullable: false),
                    GrossSalary = table.Column<double>(type: "double precision", nullable: false),
                    NetSalary = table.Column<double>(type: "double precision", nullable: false),
                    RatePerHour = table.Column<double>(type: "double precision", nullable: false),
                    Pay = table.Column<double>(type: "double precision", nullable: false),
                    Retainer = table.Column<double>(type: "double precision", nullable: false),
                    EmploymentType = table.Column<double>(type: "double precision", nullable: false),
                    HasParking = table.Column<bool>(type: "boolean", nullable: false),
                    ParkingCostPerMonth = table.Column<double>(type: "double precision", nullable: false),
                    AccountingPerMonth = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFinancialMetrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeFinancialMetricsHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    StartedAtUtc = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    Salary = table.Column<double>(type: "double precision", nullable: false),
                    HourlyCostFact = table.Column<double>(type: "double precision", nullable: false),
                    HourlyCostHand = table.Column<double>(type: "double precision", nullable: false),
                    Earnings = table.Column<double>(type: "double precision", nullable: false),
                    Expenses = table.Column<double>(type: "double precision", nullable: false),
                    Profit = table.Column<double>(type: "double precision", nullable: false),
                    ProfitAbility = table.Column<double>(type: "double precision", nullable: false),
                    GrossSalary = table.Column<double>(type: "double precision", nullable: false),
                    NetSalary = table.Column<double>(type: "double precision", nullable: false),
                    RatePerHour = table.Column<double>(type: "double precision", nullable: false),
                    Pay = table.Column<double>(type: "double precision", nullable: false),
                    Retainer = table.Column<double>(type: "double precision", nullable: false),
                    EmploymentType = table.Column<double>(type: "double precision", nullable: false),
                    HasParking = table.Column<bool>(type: "boolean", nullable: false),
                    ParkingCostPerMonth = table.Column<double>(type: "double precision", nullable: false),
                    AccountingPerMonth = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFinancialMetricsHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    WorkEmail = table.Column<string>(type: "text", nullable: false),
                    PersonalEmail = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Skype = table.Column<string>(type: "text", nullable: false),
                    Telegram = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasicSalaryParameters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RatePerHour = table.Column<double>(type: "double precision", nullable: false),
                    Pay = table.Column<double>(type: "double precision", nullable: false),
                    EmploymentType = table.Column<int>(type: "integer", nullable: false),
                    EmploymentTypeValue = table.Column<double>(type: "double precision", nullable: false),
                    HasParking = table.Column<bool>(type: "boolean", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicSalaryParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicSalaryParameters_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasicSalaryParameters_EmployeeId",
                table: "BasicSalaryParameters",
                column: "EmployeeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasicSalaryParameters");

            migrationBuilder.DropTable(
                name: "EmployeeFinancialMetrics");

            migrationBuilder.DropTable(
                name: "EmployeeFinancialMetricsHistory");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}

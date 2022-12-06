using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SalaryService.DataAccess.Migrations
{
    public partial class RenameDesiredAndReserveTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesiredFinancesAndReserve");

            migrationBuilder.CreateTable(
                name: "EstimatedFinancialEfficiency",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DesiredEarnings = table.Column<double>(type: "double precision", nullable: false),
                    DesiredProfit = table.Column<double>(type: "double precision", nullable: false),
                    DesiredProfitability = table.Column<double>(type: "double precision", nullable: false),
                    ReserveForQuarter = table.Column<double>(type: "double precision", nullable: false),
                    ReserveForHalfYear = table.Column<double>(type: "double precision", nullable: false),
                    ReserveForYear = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstimatedFinancialEfficiency", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstimatedFinancialEfficiency");

            migrationBuilder.CreateTable(
                name: "DesiredFinancesAndReserve",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DesiredEarnings = table.Column<double>(type: "double precision", nullable: false),
                    DesiredProfit = table.Column<double>(type: "double precision", nullable: false),
                    DesiredProfitability = table.Column<double>(type: "double precision", nullable: false),
                    ReserveForHalfYear = table.Column<double>(type: "double precision", nullable: false),
                    ReserveForQuarter = table.Column<double>(type: "double precision", nullable: false),
                    ReserveForYear = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesiredFinancesAndReserve", x => x.Id);
                });
        }
    }
}

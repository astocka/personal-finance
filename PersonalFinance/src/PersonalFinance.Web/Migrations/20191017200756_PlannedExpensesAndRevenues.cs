using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalFinance.Web.Migrations
{
    public partial class PlannedExpensesAndRevenues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    TotalRevenue = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PlannedExpenses = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlannedExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Kind = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    BudgetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlannedExpenses_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlannedRevenues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Kind = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    BudgetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedRevenues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlannedRevenues_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlannedExpenses_BudgetId",
                table: "PlannedExpenses",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedRevenues_BudgetId",
                table: "PlannedRevenues",
                column: "BudgetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlannedExpenses");

            migrationBuilder.DropTable(
                name: "PlannedRevenues");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Payor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                });
        }
    }
}

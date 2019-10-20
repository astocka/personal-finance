using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalFinance.Web.Migrations
{
    public partial class BudgetCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "PlannedRevenues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "PlannedExpenses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "PlannedRevenues");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "PlannedExpenses");
        }
    }
}

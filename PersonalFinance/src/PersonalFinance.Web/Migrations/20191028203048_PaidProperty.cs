using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalFinance.Web.Migrations
{
    public partial class PaidProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlannedExpenses_Budgets_BudgetId",
                table: "PlannedExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_PlannedRevenues_Budgets_BudgetId",
                table: "PlannedRevenues");

            migrationBuilder.AlterColumn<int>(
                name: "BudgetId",
                table: "PlannedRevenues",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReceived",
                table: "PlannedRevenues",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivedDate",
                table: "PlannedRevenues",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BudgetId",
                table: "PlannedExpenses",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "PlannedExpenses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidDate",
                table: "PlannedExpenses",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedExpenses_Budgets_BudgetId",
                table: "PlannedExpenses",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedRevenues_Budgets_BudgetId",
                table: "PlannedRevenues",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlannedExpenses_Budgets_BudgetId",
                table: "PlannedExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_PlannedRevenues_Budgets_BudgetId",
                table: "PlannedRevenues");

            migrationBuilder.DropColumn(
                name: "IsReceived",
                table: "PlannedRevenues");

            migrationBuilder.DropColumn(
                name: "ReceivedDate",
                table: "PlannedRevenues");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "PlannedExpenses");

            migrationBuilder.DropColumn(
                name: "PaidDate",
                table: "PlannedExpenses");

            migrationBuilder.AlterColumn<int>(
                name: "BudgetId",
                table: "PlannedRevenues",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BudgetId",
                table: "PlannedExpenses",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedExpenses_Budgets_BudgetId",
                table: "PlannedExpenses",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedRevenues_Budgets_BudgetId",
                table: "PlannedRevenues",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalBudget.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransactionAndBudgetGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetGroups_Plans_PlanId",
                table: "BudgetGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BudgetItems_BudgetItemId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "BudgetItemId",
                table: "Transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PlanId",
                table: "BudgetGroups",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetGroups_Plans_PlanId",
                table: "BudgetGroups",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BudgetItems_BudgetItemId",
                table: "Transactions",
                column: "BudgetItemId",
                principalTable: "BudgetItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetGroups_Plans_PlanId",
                table: "BudgetGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BudgetItems_BudgetItemId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "BudgetItemId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlanId",
                table: "BudgetGroups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetGroups_Plans_PlanId",
                table: "BudgetGroups",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BudgetItems_BudgetItemId",
                table: "Transactions",
                column: "BudgetItemId",
                principalTable: "BudgetItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

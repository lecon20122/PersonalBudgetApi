using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalBudget.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBudgetGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "BudgetGroups");

            migrationBuilder.DropColumn(
                name: "TotalPlanned",
                table: "BudgetGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BudgetGroups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPlanned",
                table: "BudgetGroups",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

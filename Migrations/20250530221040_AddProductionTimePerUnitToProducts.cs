using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductionManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddProductionTimePerUnitToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductionTimePerUnit",
                table: "WorkOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "EfficiencyFactor",
                table: "ProductionLines",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionTimePerUnit",
                table: "WorkOrders");

            migrationBuilder.AlterColumn<float>(
                name: "EfficiencyFactor",
                table: "ProductionLines",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}

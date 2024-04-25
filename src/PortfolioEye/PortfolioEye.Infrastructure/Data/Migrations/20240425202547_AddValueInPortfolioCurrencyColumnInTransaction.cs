using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioEye.Migrations
{
    /// <inheritdoc />
    public partial class AddValueInPortfolioCurrencyColumnInTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValueInPortfolioCurrency",
                table: "Transactions",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueInPortfolioCurrency",
                table: "Transactions");
        }
    }
}

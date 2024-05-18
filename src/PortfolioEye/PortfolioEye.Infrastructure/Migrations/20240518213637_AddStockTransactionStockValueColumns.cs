using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioEye.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStockTransactionStockValueColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceInStockCurrency",
                table: "StockTransactions",
                type: "numeric(18,6)",
                precision: 18,
                scale: 6,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "StockCurrency",
                table: "StockTransactions",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ValueInStockCurrency",
                table: "StockTransactions",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceInStockCurrency",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "StockCurrency",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "ValueInStockCurrency",
                table: "StockTransactions");
        }
    }
}

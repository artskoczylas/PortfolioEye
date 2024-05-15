using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioEye.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentStockPricesView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                 CREATE VIEW LastStockPrices AS
                                     WITH LastDates AS (
                                        SELECT "Ticker", max("Date") AS "Date"
                                        FROM public."StockPrices" SP GROUP BY "Ticker"
                                        )
                                     
                                     SELECT SP.* FROM LastDates LD
                                     INNER JOIN public."StockPrices" SP ON SP."Ticker" = LD."Ticker" AND SP."Date" = LD."Date";
                                 """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW LastStockPrices;");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioEye.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BondTransactionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueInPortfolioCurrency",
                table: "StockTransactions");

            migrationBuilder.CreateTable(
                name: "BondEmissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    Emission = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    FirstYearInterestRate = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    NextYearsInterestMargin = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BondEmissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BondTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Side = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,8)", precision: 18, scale: 8, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    PriceInPortfolioCurrency = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BondTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BondTransactions_BondEmissions_EmissionId",
                        column: x => x.EmissionId,
                        principalTable: "BondEmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BondTransactions_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BondTransactions_EmissionId",
                table: "BondTransactions",
                column: "EmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_BondTransactions_TransactionId",
                table: "BondTransactions",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BondTransactions");

            migrationBuilder.DropTable(
                name: "BondEmissions");

            migrationBuilder.AddColumn<decimal>(
                name: "ValueInPortfolioCurrency",
                table: "StockTransactions",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}

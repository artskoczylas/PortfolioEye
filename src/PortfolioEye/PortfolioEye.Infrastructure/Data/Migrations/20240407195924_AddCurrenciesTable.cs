using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioEye.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrenciesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });
            
            InserCurrency(migrationBuilder,"PLN");
            InserCurrency(migrationBuilder,"EUR");
            InserCurrency(migrationBuilder,"USD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currencies");
        }
        
        private void InserCurrency(MigrationBuilder migrationBuilder, string code)
        {
            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Code", "IsActive" },
                values: new object[] { code, true });
        }
    }
}

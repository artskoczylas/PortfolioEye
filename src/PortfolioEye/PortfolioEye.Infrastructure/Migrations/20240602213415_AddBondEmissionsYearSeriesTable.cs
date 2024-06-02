﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioEye.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBondEmissionsYearSeriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ConvertPrice",
                table: "BondEmissions",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Isin",
                table: "BondEmissions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "SaleEnd",
                table: "BondEmissions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "SaleStart",
                table: "BondEmissions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateTable(
                name: "BondEmissionsYear",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BondEmissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    No = table.Column<int>(type: "integer", nullable: false),
                    InterestRate = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BondEmissionsYear", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BondEmissionsYear_BondEmissions_BondEmissionId",
                        column: x => x.BondEmissionId,
                        principalTable: "BondEmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BondEmissionsYear_BondEmissionId",
                table: "BondEmissionsYear",
                column: "BondEmissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BondEmissionsYear");

            migrationBuilder.DropColumn(
                name: "ConvertPrice",
                table: "BondEmissions");

            migrationBuilder.DropColumn(
                name: "Isin",
                table: "BondEmissions");

            migrationBuilder.DropColumn(
                name: "SaleEnd",
                table: "BondEmissions");

            migrationBuilder.DropColumn(
                name: "SaleStart",
                table: "BondEmissions");
        }
    }
}

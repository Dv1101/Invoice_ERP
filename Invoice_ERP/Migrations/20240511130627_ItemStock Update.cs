using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice_ERP.Migrations
{
    /// <inheritdoc />
    public partial class ItemStockUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ItemsStock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ItemsStock",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ItemsStock");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ItemsStock");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderTotals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalItems",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalItems",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");
        }
    }
}

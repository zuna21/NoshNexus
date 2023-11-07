using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRestaurantImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "RestaurantImages");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "RestaurantImages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "RestaurantImages");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "RestaurantImages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

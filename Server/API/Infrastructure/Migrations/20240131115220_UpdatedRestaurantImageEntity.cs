using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRestaurantImageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullPath",
                table: "RestaurantImages");

            migrationBuilder.DropColumn(
                name: "RelativePath",
                table: "RestaurantImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullPath",
                table: "RestaurantImages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelativePath",
                table: "RestaurantImages",
                type: "text",
                nullable: true);
        }
    }
}

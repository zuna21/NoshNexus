using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantImageRelativePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "RestaurantImages",
                newName: "RelativePath");

            migrationBuilder.AddColumn<string>(
                name: "FullPath",
                table: "RestaurantImages",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullPath",
                table: "RestaurantImages");

            migrationBuilder.RenameColumn(
                name: "RelativePath",
                table: "RestaurantImages",
                newName: "Path");
        }
    }
}

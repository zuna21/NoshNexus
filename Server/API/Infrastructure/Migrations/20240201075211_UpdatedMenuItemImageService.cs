using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMenuItemImageService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullPath",
                table: "MenuItemImages");

            migrationBuilder.RenameColumn(
                name: "RelativePath",
                table: "MenuItemImages",
                newName: "ContainerName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContainerName",
                table: "MenuItemImages",
                newName: "RelativePath");

            migrationBuilder.AddColumn<string>(
                name: "FullPath",
                table: "MenuItemImages",
                type: "text",
                nullable: true);
        }
    }
}

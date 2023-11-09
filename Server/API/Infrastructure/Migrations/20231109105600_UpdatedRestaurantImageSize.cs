using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRestaurantImageSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "RestaurantImages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "RestaurantImages",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}

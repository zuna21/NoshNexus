using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantCountryRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Restaurants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_CountryId",
                table: "Restaurants",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Countries_CountryId",
                table: "Restaurants",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Countries_CountryId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_CountryId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Restaurants");
        }
    }
}

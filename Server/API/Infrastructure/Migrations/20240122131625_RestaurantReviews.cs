using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RestaurantReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RestaurantId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Review = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantReviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantReviews_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantReviews_CustomerId",
                table: "RestaurantReviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantReviews_RestaurantId",
                table: "RestaurantReviews",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestaurantReviews");
        }
    }
}

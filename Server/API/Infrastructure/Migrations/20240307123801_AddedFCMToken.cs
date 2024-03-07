﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedFCMToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FcmToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FcmToken",
                table: "AspNetUsers");
        }
    }
}

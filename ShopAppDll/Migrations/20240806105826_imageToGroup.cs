﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopAppDll.Migrations
{
    /// <inheritdoc />
    public partial class imageToGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Groups",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Groups");
        }
    }
}

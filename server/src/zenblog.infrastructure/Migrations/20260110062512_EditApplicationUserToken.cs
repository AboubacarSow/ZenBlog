using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zenblog.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditApplicationUserToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "SocialMedias",
                newName: "AddressUrl");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiresTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiresTime",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AddressUrl",
                table: "SocialMedias",
                newName: "Url");
        }
    }
}

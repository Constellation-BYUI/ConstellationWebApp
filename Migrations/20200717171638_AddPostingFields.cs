using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstellationWebApp.Migrations
{
    public partial class AddPostingFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApplicationDeadline",
                table: "Posting",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationURL",
                table: "Posting",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Posting",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationDeadline",
                table: "Posting");

            migrationBuilder.DropColumn(
                name: "ApplicationURL",
                table: "Posting");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Posting");
        }
    }
}

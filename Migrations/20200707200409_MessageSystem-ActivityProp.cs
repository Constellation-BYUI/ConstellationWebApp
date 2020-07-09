using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstellationWebApp.Migrations
{
    public partial class MessageSystemActivityProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastActivity",
                table: "Chat",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastActivity",
                table: "Chat");
        }
    }
}

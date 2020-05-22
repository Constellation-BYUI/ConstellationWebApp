using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstellationWebApp.Migrations
{
    public partial class StarredUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StarredUser",
                columns: table => new
                {
                    StarredUserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserStarredID = table.Column<string>(nullable: true),
                    StarredOwnerID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarredUser", x => x.StarredUserID);
                    table.ForeignKey(
                        name: "FK_StarredUser_AspNetUsers_StarredOwnerID",
                        column: x => x.StarredOwnerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StarredUser_AspNetUsers_UserStarredID",
                        column: x => x.UserStarredID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StarredUser_StarredOwnerID",
                table: "StarredUser",
                column: "StarredOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_StarredUser_UserStarredID",
                table: "StarredUser",
                column: "UserStarredID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StarredUser");
        }
    }
}

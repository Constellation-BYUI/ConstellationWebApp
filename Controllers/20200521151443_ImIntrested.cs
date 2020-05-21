using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstellationWebApp.Migrations
{
    public partial class ImIntrested : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IntrestedCandidates",
                columns: table => new
                {
                    IntrestedCandidateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    PostingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntrestedCandidates", x => x.IntrestedCandidateID);
                    table.ForeignKey(
                        name: "FK_IntrestedCandidates_Posting_PostingID",
                        column: x => x.PostingID,
                        principalTable: "Posting",
                        principalColumn: "PostingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntrestedCandidates_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntrestedCandidates_PostingID",
                table: "IntrestedCandidates",
                column: "PostingID");

            migrationBuilder.CreateIndex(
                name: "IX_IntrestedCandidates_UserID",
                table: "IntrestedCandidates",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntrestedCandidates");
        }
    }
}

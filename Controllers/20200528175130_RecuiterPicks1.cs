using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstellationWebApp.Migrations
{
    public partial class RecuiterPicks1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecruiterPicks",
                columns: table => new
                {
                    RecuiterPicksID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListTitle = table.Column<string>(nullable: true),
                    RecuiterID = table.Column<string>(nullable: true),
                    CandidateID = table.Column<string>(nullable: true),
                    PostingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruiterPicks", x => x.RecuiterPicksID);
                    table.ForeignKey(
                        name: "FK_RecruiterPicks_AspNetUsers_CandidateID",
                        column: x => x.CandidateID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecruiterPicks_Posting_PostingID",
                        column: x => x.PostingID,
                        principalTable: "Posting",
                        principalColumn: "PostingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecruiterPicks_AspNetUsers_RecuiterID",
                        column: x => x.RecuiterID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecruiterPicks_CandidateID",
                table: "RecruiterPicks",
                column: "CandidateID");

            migrationBuilder.CreateIndex(
                name: "IX_RecruiterPicks_PostingID",
                table: "RecruiterPicks",
                column: "PostingID");

            migrationBuilder.CreateIndex(
                name: "IX_RecruiterPicks_RecuiterID",
                table: "RecruiterPicks",
                column: "RecuiterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecruiterPicks");
        }
    }
}

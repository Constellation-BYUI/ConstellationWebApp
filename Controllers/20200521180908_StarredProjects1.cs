using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstellationWebApp.Migrations
{
    public partial class StarredProjects1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntrestedCandidates_Posting_PostingID",
                table: "IntrestedCandidates");

            migrationBuilder.DropForeignKey(
                name: "FK_IntrestedCandidates_AspNetUsers_UserID",
                table: "IntrestedCandidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Posting_PostingType_PostingTypes_PostingTypeID",
                table: "Posting_PostingType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostingTypes",
                table: "PostingTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IntrestedCandidates",
                table: "IntrestedCandidates");

            migrationBuilder.RenameTable(
                name: "PostingTypes",
                newName: "PostingType");

            migrationBuilder.RenameTable(
                name: "IntrestedCandidates",
                newName: "IntrestedCandidate");

            migrationBuilder.RenameIndex(
                name: "IX_IntrestedCandidates_UserID",
                table: "IntrestedCandidate",
                newName: "IX_IntrestedCandidate_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_IntrestedCandidates_PostingID",
                table: "IntrestedCandidate",
                newName: "IX_IntrestedCandidate_PostingID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostingType",
                table: "PostingType",
                column: "PostingTypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntrestedCandidate",
                table: "IntrestedCandidate",
                column: "IntrestedCandidateID");

            migrationBuilder.CreateTable(
                name: "StarredProject",
                columns: table => new
                {
                    StarredProjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    ProjectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarredProject", x => x.StarredProjectID);
                    table.ForeignKey(
                        name: "FK_StarredProject_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StarredProject_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StarredProject_ProjectID",
                table: "StarredProject",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_StarredProject_UserID",
                table: "StarredProject",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_IntrestedCandidate_Posting_PostingID",
                table: "IntrestedCandidate",
                column: "PostingID",
                principalTable: "Posting",
                principalColumn: "PostingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntrestedCandidate_AspNetUsers_UserID",
                table: "IntrestedCandidate",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posting_PostingType_PostingType_PostingTypeID",
                table: "Posting_PostingType",
                column: "PostingTypeID",
                principalTable: "PostingType",
                principalColumn: "PostingTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntrestedCandidate_Posting_PostingID",
                table: "IntrestedCandidate");

            migrationBuilder.DropForeignKey(
                name: "FK_IntrestedCandidate_AspNetUsers_UserID",
                table: "IntrestedCandidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Posting_PostingType_PostingType_PostingTypeID",
                table: "Posting_PostingType");

            migrationBuilder.DropTable(
                name: "StarredProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostingType",
                table: "PostingType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IntrestedCandidate",
                table: "IntrestedCandidate");

            migrationBuilder.RenameTable(
                name: "PostingType",
                newName: "PostingTypes");

            migrationBuilder.RenameTable(
                name: "IntrestedCandidate",
                newName: "IntrestedCandidates");

            migrationBuilder.RenameIndex(
                name: "IX_IntrestedCandidate_UserID",
                table: "IntrestedCandidates",
                newName: "IX_IntrestedCandidates_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_IntrestedCandidate_PostingID",
                table: "IntrestedCandidates",
                newName: "IX_IntrestedCandidates_PostingID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostingTypes",
                table: "PostingTypes",
                column: "PostingTypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntrestedCandidates",
                table: "IntrestedCandidates",
                column: "IntrestedCandidateID");

            migrationBuilder.AddForeignKey(
                name: "FK_IntrestedCandidates_Posting_PostingID",
                table: "IntrestedCandidates",
                column: "PostingID",
                principalTable: "Posting",
                principalColumn: "PostingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntrestedCandidates_AspNetUsers_UserID",
                table: "IntrestedCandidates",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posting_PostingType_PostingTypes_PostingTypeID",
                table: "Posting_PostingType",
                column: "PostingTypeID",
                principalTable: "PostingTypes",
                principalColumn: "PostingTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

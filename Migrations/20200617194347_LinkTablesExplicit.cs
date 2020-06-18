using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstellationWebApp.Migrations
{
    public partial class LinkTablesExplicit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posting_PostingType_PostingType_PostingTypeID",
                table: "Posting_PostingType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostingType",
                table: "PostingType");

            migrationBuilder.RenameTable(
                name: "PostingType",
                newName: "SkillLink");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillLink",
                table: "SkillLink",
                column: "PostingTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posting_PostingType_SkillLink_PostingTypeID",
                table: "Posting_PostingType",
                column: "PostingTypeID",
                principalTable: "SkillLink",
                principalColumn: "PostingTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posting_PostingType_SkillLink_PostingTypeID",
                table: "Posting_PostingType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillLink",
                table: "SkillLink");

            migrationBuilder.RenameTable(
                name: "SkillLink",
                newName: "PostingType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostingType",
                table: "PostingType",
                column: "PostingTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posting_PostingType_PostingType_PostingTypeID",
                table: "Posting_PostingType",
                column: "PostingTypeID",
                principalTable: "PostingType",
                principalColumn: "PostingTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

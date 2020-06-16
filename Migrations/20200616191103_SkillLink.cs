using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstellationWebApp.Migrations
{
    public partial class SkillLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkillLinkID",
                table: "UserSkill",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SkillLinks",
                columns: table => new
                {
                    SkillLinkID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillLinkUrl = table.Column<string>(nullable: true),
                    SkilLinkLabel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillLinks", x => x.SkillLinkID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_SkillLinkID",
                table: "UserSkill",
                column: "SkillLinkID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_SkillLinks_SkillLinkID",
                table: "UserSkill",
                column: "SkillLinkID",
                principalTable: "SkillLinks",
                principalColumn: "SkillLinkID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_SkillLinks_SkillLinkID",
                table: "UserSkill");

            migrationBuilder.DropTable(
                name: "SkillLinks");

            migrationBuilder.DropIndex(
                name: "IX_UserSkill_SkillLinkID",
                table: "UserSkill");

            migrationBuilder.DropColumn(
                name: "SkillLinkID",
                table: "UserSkill");
        }
    }
}

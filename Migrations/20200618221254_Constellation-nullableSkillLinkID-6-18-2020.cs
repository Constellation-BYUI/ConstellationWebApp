using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstellationWebApp.Migrations
{
    public partial class ConstellationnullableSkillLinkID6182020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_SkillLink_SkillLinkID",
                table: "UserSkill");

            migrationBuilder.DropIndex(
                name: "IX_UserSkill_SkillLinkID",
                table: "UserSkill");

            migrationBuilder.AlterColumn<int>(
                name: "SkillLinkID",
                table: "UserSkill",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_SkillLinkID",
                table: "UserSkill",
                column: "SkillLinkID",
                unique: true,
                filter: "[SkillLinkID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_SkillLink_SkillLinkID",
                table: "UserSkill",
                column: "SkillLinkID",
                principalTable: "SkillLink",
                principalColumn: "SkillLinkID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_SkillLink_SkillLinkID",
                table: "UserSkill");

            migrationBuilder.DropIndex(
                name: "IX_UserSkill_SkillLinkID",
                table: "UserSkill");

            migrationBuilder.AlterColumn<int>(
                name: "SkillLinkID",
                table: "UserSkill",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_SkillLinkID",
                table: "UserSkill",
                column: "SkillLinkID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_SkillLink_SkillLinkID",
                table: "UserSkill",
                column: "SkillLinkID",
                principalTable: "SkillLink",
                principalColumn: "SkillLinkID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

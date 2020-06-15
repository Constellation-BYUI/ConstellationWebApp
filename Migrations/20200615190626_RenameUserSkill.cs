using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstellationWebApp.Migrations
{
    public partial class RenameUserSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discipline_Skill_SkillID",
                table: "Discipline");

            migrationBuilder.DropForeignKey(
                name: "FK_Discipline_AspNetUsers_UserID",
                table: "Discipline");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discipline",
                table: "Discipline");

            migrationBuilder.RenameTable(
                name: "Discipline",
                newName: "UserSkill");

            migrationBuilder.RenameIndex(
                name: "IX_Discipline_SkillID",
                table: "UserSkill",
                newName: "IX_UserSkill_SkillID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSkill",
                table: "UserSkill",
                columns: new[] { "UserID", "SkillID" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_Skill_SkillID",
                table: "UserSkill",
                column: "SkillID",
                principalTable: "Skill",
                principalColumn: "SkillID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_AspNetUsers_UserID",
                table: "UserSkill",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_Skill_SkillID",
                table: "UserSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_AspNetUsers_UserID",
                table: "UserSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSkill",
                table: "UserSkill");

            migrationBuilder.RenameTable(
                name: "UserSkill",
                newName: "Discipline");

            migrationBuilder.RenameIndex(
                name: "IX_UserSkill_SkillID",
                table: "Discipline",
                newName: "IX_Discipline_SkillID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discipline",
                table: "Discipline",
                columns: new[] { "UserID", "SkillID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Discipline_Skill_SkillID",
                table: "Discipline",
                column: "SkillID",
                principalTable: "Skill",
                principalColumn: "SkillID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Discipline_AspNetUsers_UserID",
                table: "Discipline",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

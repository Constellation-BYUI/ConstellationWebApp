using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstellationWebApp.Migrations
{
    public partial class ConstellationUserSkillLinkTable6182020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactLinks_AspNetUsers_UsersId",
                table: "ContactLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLinks_Project_ProjectsProjectID",
                table: "ProjectLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_SkillLink_SkillLinkID",
                table: "UserSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_AspNetUsers_UserID",
                table: "UserSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSkill",
                table: "UserSkill");

            migrationBuilder.DropIndex(
                name: "IX_UserSkill_SkillLinkID",
                table: "UserSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectLinks",
                table: "ProjectLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactLinks",
                table: "ContactLinks");

            migrationBuilder.DropColumn(
                name: "SkillLinkID",
                table: "UserSkill");

            migrationBuilder.RenameTable(
                name: "ProjectLinks",
                newName: "ProjectLink");

            migrationBuilder.RenameTable(
                name: "ContactLinks",
                newName: "ContactLink");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectLinks_ProjectsProjectID",
                table: "ProjectLink",
                newName: "IX_ProjectLink_ProjectsProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactLinks_UsersId",
                table: "ContactLink",
                newName: "IX_ContactLink_UsersId");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "UserSkill",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "UserSkillID",
                table: "UserSkill",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSkill",
                table: "UserSkill",
                column: "UserSkillID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectLink",
                table: "ProjectLink",
                column: "ProjectLinkID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactLink",
                table: "ContactLink",
                column: "ContactLinkID");

            migrationBuilder.CreateTable(
                name: "UserSkillLink",
                columns: table => new
                {
                    UserSkillLinkID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillID = table.Column<int>(nullable: false),
                    UserSkillID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkillLink", x => x.UserSkillLinkID);
                    table.ForeignKey(
                        name: "FK_UserSkillLink_SkillLink_SkillID",
                        column: x => x.SkillID,
                        principalTable: "SkillLink",
                        principalColumn: "SkillLinkID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkillLink_UserSkill_UserSkillID",
                        column: x => x.UserSkillID,
                        principalTable: "UserSkill",
                        principalColumn: "UserSkillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_UserID",
                table: "UserSkill",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkillLink_SkillID",
                table: "UserSkillLink",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkillLink_UserSkillID",
                table: "UserSkillLink",
                column: "UserSkillID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactLink_AspNetUsers_UsersId",
                table: "ContactLink",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLink_Project_ProjectsProjectID",
                table: "ProjectLink",
                column: "ProjectsProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_AspNetUsers_UserID",
                table: "UserSkill",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactLink_AspNetUsers_UsersId",
                table: "ContactLink");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLink_Project_ProjectsProjectID",
                table: "ProjectLink");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_AspNetUsers_UserID",
                table: "UserSkill");

            migrationBuilder.DropTable(
                name: "UserSkillLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSkill",
                table: "UserSkill");

            migrationBuilder.DropIndex(
                name: "IX_UserSkill_UserID",
                table: "UserSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectLink",
                table: "ProjectLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactLink",
                table: "ContactLink");

            migrationBuilder.DropColumn(
                name: "UserSkillID",
                table: "UserSkill");

            migrationBuilder.RenameTable(
                name: "ProjectLink",
                newName: "ProjectLinks");

            migrationBuilder.RenameTable(
                name: "ContactLink",
                newName: "ContactLinks");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectLink_ProjectsProjectID",
                table: "ProjectLinks",
                newName: "IX_ProjectLinks_ProjectsProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactLink_UsersId",
                table: "ContactLinks",
                newName: "IX_ContactLinks_UsersId");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "UserSkill",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SkillLinkID",
                table: "UserSkill",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSkill",
                table: "UserSkill",
                columns: new[] { "UserID", "SkillID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectLinks",
                table: "ProjectLinks",
                column: "ProjectLinkID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactLinks",
                table: "ContactLinks",
                column: "ContactLinkID");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_SkillLinkID",
                table: "UserSkill",
                column: "SkillLinkID",
                unique: true,
                filter: "[SkillLinkID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactLinks_AspNetUsers_UsersId",
                table: "ContactLinks",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLinks_Project_ProjectsProjectID",
                table: "ProjectLinks",
                column: "ProjectsProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_SkillLink_SkillLinkID",
                table: "UserSkill",
                column: "SkillLinkID",
                principalTable: "SkillLink",
                principalColumn: "SkillLinkID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_AspNetUsers_UserID",
                table: "UserSkill",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

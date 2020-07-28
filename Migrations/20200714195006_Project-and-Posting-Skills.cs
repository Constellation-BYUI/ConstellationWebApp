using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstellationWebApp.Migrations
{
    public partial class ProjectandPostingSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CollaborationTitle",
                table: "UserProjects",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MessageText",
                table: "Message",
                maxLength: 550,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PostingSkills",
                columns: table => new
                {
                    SkillID = table.Column<int>(nullable: false),
                    PostingID = table.Column<int>(nullable: false),
                    PriorityLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostingSkills", x => new { x.SkillID, x.PostingID });
                    table.ForeignKey(
                        name: "FK_PostingSkills_Posting_PostingID",
                        column: x => x.PostingID,
                        principalTable: "Posting",
                        principalColumn: "PostingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostingSkills_Skill_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skill",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSkills",
                columns: table => new
                {
                    SkillID = table.Column<int>(nullable: false),
                    ProjectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSkills", x => new { x.SkillID, x.ProjectID });
                    table.ForeignKey(
                        name: "FK_ProjectSkills_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSkills_Skill_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skill",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostingSkills_PostingID",
                table: "PostingSkills",
                column: "PostingID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSkills_ProjectID",
                table: "ProjectSkills",
                column: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostingSkills");

            migrationBuilder.DropTable(
                name: "ProjectSkills");

            migrationBuilder.DropColumn(
                name: "CollaborationTitle",
                table: "UserProjects");

            migrationBuilder.AlterColumn<string>(
                name: "MessageText",
                table: "Message",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 550,
                oldNullable: true);
        }
    }
}

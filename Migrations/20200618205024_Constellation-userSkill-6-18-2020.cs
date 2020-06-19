using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstellationWebApp.Migrations
{
    public partial class ConstellationuserSkill6182020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Bio = table.Column<string>(maxLength: 2000, nullable: true),
                    Seeking = table.Column<string>(maxLength: 150, nullable: true),
                    PhotoPath = table.Column<string>(nullable: true),
                    ResumePhotoPath = table.Column<string>(nullable: true),
                    displayMyProfile = table.Column<bool>(nullable: true),
                    AreaOfDiscipline = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discipline",
                columns: table => new
                {
                    DisciplineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplineName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline", x => x.DisciplineID);
                });

            migrationBuilder.CreateTable(
                name: "PostingType",
                columns: table => new
                {
                    PostingTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostingTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostingType", x => x.PostingTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectID);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillID);
                });

            migrationBuilder.CreateTable(
                name: "SkillLink",
                columns: table => new
                {
                    SkillLinkID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillLinkUrl = table.Column<string>(nullable: true),
                    SkilLinkLabel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillLink", x => x.SkillLinkID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactLinks",
                columns: table => new
                {
                    ContactLinkID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactLinkLabel = table.Column<string>(maxLength: 50, nullable: true),
                    ContactLinkUrl = table.Column<string>(nullable: true),
                    UsersId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactLinks", x => x.ContactLinkID);
                    table.ForeignKey(
                        name: "FK_ContactLinks_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posting",
                columns: table => new
                {
                    PostingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostingTitle = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    PostingFor = table.Column<string>(nullable: false),
                    PostingOwnerId = table.Column<string>(nullable: true),
                    SharableToTeam = table.Column<bool>(nullable: false),
                    HidePosting = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posting", x => x.PostingID);
                    table.ForeignKey(
                        name: "FK_Posting_AspNetUsers_PostingOwnerId",
                        column: x => x.PostingOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "ProjectLinks",
                columns: table => new
                {
                    ProjectLinkID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectLinkLabel = table.Column<string>(maxLength: 50, nullable: true),
                    ProjectLinkUrl = table.Column<string>(nullable: true),
                    ProjectsProjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectLinks", x => x.ProjectLinkID);
                    table.ForeignKey(
                        name: "FK_ProjectLinks_Project_ProjectsProjectID",
                        column: x => x.ProjectsProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "UserProjects",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    ProjectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjects", x => new { x.UserID, x.ProjectID });
                    table.ForeignKey(
                        name: "FK_UserProjects_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProjects_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillDiscipline",
                columns: table => new
                {
                    DisciplineID = table.Column<int>(nullable: false),
                    SkillID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillDiscipline", x => new { x.DisciplineID, x.SkillID });
                    table.ForeignKey(
                        name: "FK_SkillDiscipline_Discipline_DisciplineID",
                        column: x => x.DisciplineID,
                        principalTable: "Discipline",
                        principalColumn: "DisciplineID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillDiscipline_Skill_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skill",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSkill",
                columns: table => new
                {
                    SkillID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: false),
                    SkillLinkID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkill", x => new { x.UserID, x.SkillID });
                    table.ForeignKey(
                        name: "FK_UserSkill_Skill_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skill",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkill_SkillLink_SkillLinkID",
                        column: x => x.SkillLinkID,
                        principalTable: "SkillLink",
                        principalColumn: "SkillLinkID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkill_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntrestedCandidate",
                columns: table => new
                {
                    IntrestedCandidateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    PostingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntrestedCandidate", x => x.IntrestedCandidateID);
                    table.ForeignKey(
                        name: "FK_IntrestedCandidate_Posting_PostingID",
                        column: x => x.PostingID,
                        principalTable: "Posting",
                        principalColumn: "PostingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntrestedCandidate_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posting_PostingType",
                columns: table => new
                {
                    Posting_PostingTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostingID = table.Column<int>(nullable: false),
                    PostingTypeID = table.Column<int>(nullable: false),
                    Assigned = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posting_PostingType", x => x.Posting_PostingTypeID);
                    table.ForeignKey(
                        name: "FK_Posting_PostingType_Posting_PostingID",
                        column: x => x.PostingID,
                        principalTable: "Posting",
                        principalColumn: "PostingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posting_PostingType_PostingType_PostingTypeID",
                        column: x => x.PostingTypeID,
                        principalTable: "PostingType",
                        principalColumn: "PostingTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPosting",
                columns: table => new
                {
                    ProjectPostingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(nullable: false),
                    PostingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPosting", x => x.ProjectPostingID);
                    table.ForeignKey(
                        name: "FK_ProjectPosting_Posting_PostingID",
                        column: x => x.PostingID,
                        principalTable: "Posting",
                        principalColumn: "PostingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectPosting_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "StarredPosting",
                columns: table => new
                {
                    StarredPostingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    PostingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarredPosting", x => x.StarredPostingID);
                    table.ForeignKey(
                        name: "FK_StarredPosting_Posting_PostingID",
                        column: x => x.PostingID,
                        principalTable: "Posting",
                        principalColumn: "PostingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StarredPosting_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContactLinks_UsersId",
                table: "ContactLinks",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_IntrestedCandidate_PostingID",
                table: "IntrestedCandidate",
                column: "PostingID");

            migrationBuilder.CreateIndex(
                name: "IX_IntrestedCandidate_UserID",
                table: "IntrestedCandidate",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Posting_PostingOwnerId",
                table: "Posting",
                column: "PostingOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Posting_PostingType_PostingID",
                table: "Posting_PostingType",
                column: "PostingID");

            migrationBuilder.CreateIndex(
                name: "IX_Posting_PostingType_PostingTypeID",
                table: "Posting_PostingType",
                column: "PostingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLinks_ProjectsProjectID",
                table: "ProjectLinks",
                column: "ProjectsProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPosting_PostingID",
                table: "ProjectPosting",
                column: "PostingID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPosting_ProjectID",
                table: "ProjectPosting",
                column: "ProjectID");

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

            migrationBuilder.CreateIndex(
                name: "IX_SkillDiscipline_SkillID",
                table: "SkillDiscipline",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_StarredPosting_PostingID",
                table: "StarredPosting",
                column: "PostingID");

            migrationBuilder.CreateIndex(
                name: "IX_StarredPosting_UserID",
                table: "StarredPosting",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_StarredProject_ProjectID",
                table: "StarredProject",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_StarredProject_UserID",
                table: "StarredProject",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_StarredUser_StarredOwnerID",
                table: "StarredUser",
                column: "StarredOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_StarredUser_UserStarredID",
                table: "StarredUser",
                column: "UserStarredID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjects_ProjectID",
                table: "UserProjects",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_SkillID",
                table: "UserSkill",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_SkillLinkID",
                table: "UserSkill",
                column: "SkillLinkID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ContactLinks");

            migrationBuilder.DropTable(
                name: "IntrestedCandidate");

            migrationBuilder.DropTable(
                name: "Posting_PostingType");

            migrationBuilder.DropTable(
                name: "ProjectLinks");

            migrationBuilder.DropTable(
                name: "ProjectPosting");

            migrationBuilder.DropTable(
                name: "RecruiterPicks");

            migrationBuilder.DropTable(
                name: "SkillDiscipline");

            migrationBuilder.DropTable(
                name: "StarredPosting");

            migrationBuilder.DropTable(
                name: "StarredProject");

            migrationBuilder.DropTable(
                name: "StarredUser");

            migrationBuilder.DropTable(
                name: "UserProjects");

            migrationBuilder.DropTable(
                name: "UserSkill");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PostingType");

            migrationBuilder.DropTable(
                name: "Discipline");

            migrationBuilder.DropTable(
                name: "Posting");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "SkillLink");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

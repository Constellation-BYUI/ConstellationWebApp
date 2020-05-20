using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstellationWebApp.Migrations
{
    public partial class Posting1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posting",
                columns: table => new
                {
                    PostingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    PostingFor = table.Column<string>(nullable: true),
                    PostingOwnerId = table.Column<string>(nullable: true)
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
                name: "PostingTypes",
                columns: table => new
                {
                    PostingTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostingTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostingTypes", x => x.PostingTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Posting_PostingType",
                columns: table => new
                {
                    Posting_PostingTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostingID = table.Column<int>(nullable: false),
                    PostingTypeID = table.Column<int>(nullable: false)
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
                        name: "FK_Posting_PostingType_PostingTypes_PostingTypeID",
                        column: x => x.PostingTypeID,
                        principalTable: "PostingTypes",
                        principalColumn: "PostingTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posting_PostingType");

            migrationBuilder.DropTable(
                name: "Posting");

            migrationBuilder.DropTable(
                name: "PostingTypes");
        }
    }
}

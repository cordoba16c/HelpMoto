using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HelpMoto.Web.Migrations
{
    public partial class Completeall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_WorkshopTypes_WorkshopTypeId",
                table: "Histories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkshopTypes",
                table: "WorkshopTypes");

            migrationBuilder.RenameTable(
                name: "WorkshopTypes",
                newName: "WorkshopType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkshopType",
                table: "WorkshopType",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Workshop",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    ContactName = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneName = table.Column<string>(maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    WorkshopTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workshop_WorkshopType_WorkshopTypeId",
                        column: x => x.WorkshopTypeId,
                        principalTable: "WorkshopType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workshop_WorkshopTypeId",
                table: "Workshop",
                column: "WorkshopTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_WorkshopType_WorkshopTypeId",
                table: "Histories",
                column: "WorkshopTypeId",
                principalTable: "WorkshopType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_WorkshopType_WorkshopTypeId",
                table: "Histories");

            migrationBuilder.DropTable(
                name: "Workshop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkshopType",
                table: "WorkshopType");

            migrationBuilder.RenameTable(
                name: "WorkshopType",
                newName: "WorkshopTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkshopTypes",
                table: "WorkshopTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_WorkshopTypes_WorkshopTypeId",
                table: "Histories",
                column: "WorkshopTypeId",
                principalTable: "WorkshopTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

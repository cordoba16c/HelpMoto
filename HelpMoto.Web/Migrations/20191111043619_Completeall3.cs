using Microsoft.EntityFrameworkCore.Migrations;

namespace HelpMoto.Web.Migrations
{
    public partial class Completeall3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_WorkshopType_WorkshopTypeId",
                table: "Histories");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshop_WorkshopType_WorkshopTypeId",
                table: "Workshop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkshopType",
                table: "WorkshopType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workshop",
                table: "Workshop");

            migrationBuilder.RenameTable(
                name: "WorkshopType",
                newName: "WorkshopTypes");

            migrationBuilder.RenameTable(
                name: "Workshop",
                newName: "Workshops");

            migrationBuilder.RenameIndex(
                name: "IX_Workshop_WorkshopTypeId",
                table: "Workshops",
                newName: "IX_Workshops_WorkshopTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkshopTypes",
                table: "WorkshopTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workshops",
                table: "Workshops",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_WorkshopTypes_WorkshopTypeId",
                table: "Histories",
                column: "WorkshopTypeId",
                principalTable: "WorkshopTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workshops_WorkshopTypes_WorkshopTypeId",
                table: "Workshops",
                column: "WorkshopTypeId",
                principalTable: "WorkshopTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_WorkshopTypes_WorkshopTypeId",
                table: "Histories");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshops_WorkshopTypes_WorkshopTypeId",
                table: "Workshops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkshopTypes",
                table: "WorkshopTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workshops",
                table: "Workshops");

            migrationBuilder.RenameTable(
                name: "WorkshopTypes",
                newName: "WorkshopType");

            migrationBuilder.RenameTable(
                name: "Workshops",
                newName: "Workshop");

            migrationBuilder.RenameIndex(
                name: "IX_Workshops_WorkshopTypeId",
                table: "Workshop",
                newName: "IX_Workshop_WorkshopTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkshopType",
                table: "WorkshopType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workshop",
                table: "Workshop",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_WorkshopType_WorkshopTypeId",
                table: "Histories",
                column: "WorkshopTypeId",
                principalTable: "WorkshopType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workshop_WorkshopType_WorkshopTypeId",
                table: "Workshop",
                column: "WorkshopTypeId",
                principalTable: "WorkshopType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

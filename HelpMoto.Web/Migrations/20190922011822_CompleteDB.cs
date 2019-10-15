using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HelpMoto.Web.Migrations
{
    public partial class CompleteDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Document",
                table: "Owners",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "CellPhone",
                table: "Owners",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.CreateTable(
                name: "Concessionaires",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Ownerid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concessionaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Concessionaires_Owners_Ownerid",
                        column: x => x.Ownerid,
                        principalTable: "Owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtraServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Ownerid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraServices_Owners_Ownerid",
                        column: x => x.Ownerid,
                        principalTable: "Owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MotorcycleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceSellingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceSellingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkshopTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorcycles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    MotorcycleTypeId = table.Column<int>(nullable: true),
                    Ownerid = table.Column<int>(nullable: true),
                    Shop = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motorcycles_MotorcycleTypes_MotorcycleTypeId",
                        column: x => x.MotorcycleTypeId,
                        principalTable: "MotorcycleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Motorcycles_Owners_Ownerid",
                        column: x => x.Ownerid,
                        principalTable: "Owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CraneServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    MotorcycleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CraneServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CraneServices_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlaceSellings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PlaceSellingTypeId = table.Column<int>(nullable: true),
                    Ownerid = table.Column<int>(nullable: true),
                    MotorcycleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceSellings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceSellings_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlaceSellings_Owners_Ownerid",
                        column: x => x.Ownerid,
                        principalTable: "Owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlaceSellings_PlaceSellingTypes_PlaceSellingTypeId",
                        column: x => x.PlaceSellingTypeId,
                        principalTable: "PlaceSellingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkshopServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    WorkshopTypeId = table.Column<int>(nullable: true),
                    MotorcycleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkshopServices_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkshopServices_WorkshopTypes_WorkshopTypeId",
                        column: x => x.WorkshopTypeId,
                        principalTable: "WorkshopTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Concessionaires_Ownerid",
                table: "Concessionaires",
                column: "Ownerid");

            migrationBuilder.CreateIndex(
                name: "IX_CraneServices_MotorcycleId",
                table: "CraneServices",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraServices_Ownerid",
                table: "ExtraServices",
                column: "Ownerid");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_MotorcycleTypeId",
                table: "Motorcycles",
                column: "MotorcycleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_Ownerid",
                table: "Motorcycles",
                column: "Ownerid");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceSellings_MotorcycleId",
                table: "PlaceSellings",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceSellings_Ownerid",
                table: "PlaceSellings",
                column: "Ownerid");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceSellings_PlaceSellingTypeId",
                table: "PlaceSellings",
                column: "PlaceSellingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopServices_MotorcycleId",
                table: "WorkshopServices",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopServices_WorkshopTypeId",
                table: "WorkshopServices",
                column: "WorkshopTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Concessionaires");

            migrationBuilder.DropTable(
                name: "CraneServices");

            migrationBuilder.DropTable(
                name: "ExtraServices");

            migrationBuilder.DropTable(
                name: "PlaceSellings");

            migrationBuilder.DropTable(
                name: "WorkshopServices");

            migrationBuilder.DropTable(
                name: "PlaceSellingTypes");

            migrationBuilder.DropTable(
                name: "Motorcycles");

            migrationBuilder.DropTable(
                name: "WorkshopTypes");

            migrationBuilder.DropTable(
                name: "MotorcycleTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                table: "Owners",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "CellPhone",
                table: "Owners",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}

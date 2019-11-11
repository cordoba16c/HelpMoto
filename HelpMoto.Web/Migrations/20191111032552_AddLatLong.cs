using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HelpMoto.Web.Migrations
{
    public partial class AddLatLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycles_Owners_Ownerid",
                table: "Motorcycles");

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

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Owners",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Ownerid",
                table: "Motorcycles",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Motorcycles_Ownerid",
                table: "Motorcycles",
                newName: "IX_Motorcycles_OwnerId");

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    InicialDate = table.Column<DateTime>(nullable: false),
                    FinalDate = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    WorkshopTypeId = table.Column<int>(nullable: true),
                    MotorcycleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Histories_WorkshopTypes_WorkshopTypeId",
                        column: x => x.WorkshopTypeId,
                        principalTable: "WorkshopTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Histories_MotorcycleId",
                table: "Histories",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_WorkshopTypeId",
                table: "Histories",
                column: "WorkshopTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycles_Owners_OwnerId",
                table: "Motorcycles",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycles_Owners_OwnerId",
                table: "Motorcycles");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Owners",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Motorcycles",
                newName: "Ownerid");

            migrationBuilder.RenameIndex(
                name: "IX_Motorcycles_OwnerId",
                table: "Motorcycles",
                newName: "IX_Motorcycles_Ownerid");

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
                name: "CraneServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MotorcycleId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
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
                name: "ExtraServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    Ownerid = table.Column<int>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
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
                name: "WorkshopServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MotorcycleId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    WorkshopTypeId = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "PlaceSellings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MotorcycleId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Ownerid = table.Column<int>(nullable: true),
                    PlaceSellingTypeId = table.Column<int>(nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycles_Owners_Ownerid",
                table: "Motorcycles",
                column: "Ownerid",
                principalTable: "Owners",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

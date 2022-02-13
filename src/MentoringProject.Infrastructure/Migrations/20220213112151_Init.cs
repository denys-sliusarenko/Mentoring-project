using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentoringProject.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnerCars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnerCars_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnerCars_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "Color" },
                values: new object[,]
                {
                    { new Guid("589dfdf0-60bd-4854-a7ee-91d6b30dbae9"), "Mercedes", "Black" },
                    { new Guid("c5a5ccf8-280c-4341-a9e2-678877a432df"), "BMW", "Red" },
                    { new Guid("d3dbee01-5eaa-4c80-9365-04cd007be277"), "Nissan", "White" }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("4d41b120-9dbf-48df-8325-2f9c490cf585"), "Tom", "Wolker" },
                    { new Guid("a13ae253-bd19-400d-8c74-fabb31f0fb45"), "Alice", "Wolker" },
                    { new Guid("ca67e360-bc52-4e41-9e0a-5ff6d40b1ff4"), "Adam", "Wolker" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OwnerCars_CarId",
                table: "OwnerCars",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerCars_OwnerId",
                table: "OwnerCars",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnerCars");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}

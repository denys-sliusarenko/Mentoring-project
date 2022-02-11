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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerCars", x => new { x.OwnerId, x.CarId });
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
                    { new Guid("3aba351f-87ba-46e4-86aa-2b164eeb6965"), "Mercedes", "Black" },
                    { new Guid("ba76963d-cfc4-4b49-a026-304a8f099424"), "BMW", "Red" },
                    { new Guid("dc4540e2-8bf0-4d2b-8e88-14fa7d367a42"), "Nissan", "White" }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("259844cc-770f-419d-ada4-6c7879aa0033"), "Adam", "Wolker" },
                    { new Guid("5391fc12-1596-4cf4-9db7-b6c8bd087f13"), "Tom", "Wolker" },
                    { new Guid("b41fb727-56c6-4e62-8d96-905da9dcf5c9"), "Alice", "Wolker" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OwnerCars_CarId",
                table: "OwnerCars",
                column: "CarId");
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

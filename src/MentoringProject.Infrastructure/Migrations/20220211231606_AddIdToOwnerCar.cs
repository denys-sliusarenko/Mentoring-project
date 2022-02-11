using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentoringProject.Infrastructure.Migrations
{
    public partial class AddIdToOwnerCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnerCars",
                table: "OwnerCars");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("3aba351f-87ba-46e4-86aa-2b164eeb6965"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("ba76963d-cfc4-4b49-a026-304a8f099424"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("dc4540e2-8bf0-4d2b-8e88-14fa7d367a42"));

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: new Guid("259844cc-770f-419d-ada4-6c7879aa0033"));

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: new Guid("5391fc12-1596-4cf4-9db7-b6c8bd087f13"));

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: new Guid("b41fb727-56c6-4e62-8d96-905da9dcf5c9"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "OwnerCars",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_OwnerCars_OwnerId_CarId",
                table: "OwnerCars",
                columns: new[] { "OwnerId", "CarId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnerCars",
                table: "OwnerCars",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "Color" },
                values: new object[,]
                {
                    { new Guid("089582de-6715-48dd-93d6-b011a3a68528"), "Mercedes", "Black" },
                    { new Guid("883df809-7943-4e06-91b9-cd3ed6eb8120"), "Nissan", "White" },
                    { new Guid("f8697e51-6996-47fe-aa73-421d4384b8ac"), "BMW", "Red" }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("18b0d7ed-c4ca-409f-a2db-e2ef4b914a97"), "Tom", "Wolker" },
                    { new Guid("9cedd152-67b9-44f2-bd5d-524681b4bde1"), "Adam", "Wolker" },
                    { new Guid("b270753a-b4cb-4bd8-be8c-5f3bf701cec9"), "Alice", "Wolker" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_OwnerCars_OwnerId_CarId",
                table: "OwnerCars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnerCars",
                table: "OwnerCars");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("089582de-6715-48dd-93d6-b011a3a68528"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("883df809-7943-4e06-91b9-cd3ed6eb8120"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("f8697e51-6996-47fe-aa73-421d4384b8ac"));

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: new Guid("18b0d7ed-c4ca-409f-a2db-e2ef4b914a97"));

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: new Guid("9cedd152-67b9-44f2-bd5d-524681b4bde1"));

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: new Guid("b270753a-b4cb-4bd8-be8c-5f3bf701cec9"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OwnerCars");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnerCars",
                table: "OwnerCars",
                columns: new[] { "OwnerId", "CarId" });

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
        }
    }
}

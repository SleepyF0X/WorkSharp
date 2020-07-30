using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkSharp.DAL.Migrations
{
    public partial class AddData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("0a33f156-7595-4ebc-b2b4-d7343b4ed4bc"));

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatorId", "Info", "Name" },
                values: new object[,]
                {
                    { new Guid("9d2927b1-549a-4411-a46b-df83655d5935"), new Guid("00000000-0000-0000-0000-000000000000"), "OMG", "Proj" },
                    { new Guid("ba753f24-e6b7-418f-a6f8-389baa637b77"), new Guid("00000000-0000-0000-0000-000000000000"), "OMG", "1" },
                    { new Guid("efa016f9-bf76-48e1-a5c5-1a1fb8be8782"), new Guid("00000000-0000-0000-0000-000000000000"), "OMG", "2" },
                    { new Guid("6be2116a-c96f-4a98-a182-aa41b2bd59cd"), new Guid("00000000-0000-0000-0000-000000000000"), "OMG", "3" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("6be2116a-c96f-4a98-a182-aa41b2bd59cd"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("9d2927b1-549a-4411-a46b-df83655d5935"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("ba753f24-e6b7-418f-a6f8-389baa637b77"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("efa016f9-bf76-48e1-a5c5-1a1fb8be8782"));

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatorId", "Info", "Name" },
                values: new object[] { new Guid("0a33f156-7595-4ebc-b2b4-d7343b4ed4bc"), new Guid("00000000-0000-0000-0000-000000000000"), "OMG", "Proj" });
        }
    }
}

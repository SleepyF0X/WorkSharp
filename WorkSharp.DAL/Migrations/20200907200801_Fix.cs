using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkSharp.DAL.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Projects_DbProjectId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DbProjectId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DbProjectId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DbProjectId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DbProjectId",
                table: "AspNetUsers",
                column: "DbProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Projects_DbProjectId",
                table: "AspNetUsers",
                column: "DbProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

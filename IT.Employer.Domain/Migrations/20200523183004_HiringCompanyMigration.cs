using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IT.Employer.Domain.Migrations
{
    public partial class HiringCompanyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HiringCompanyId",
                table: "Hires",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Hires_HiringCompanyId",
                table: "Hires",
                column: "HiringCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hires_Companies_HiringCompanyId",
                table: "Hires",
                column: "HiringCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hires_Companies_HiringCompanyId",
                table: "Hires");

            migrationBuilder.DropIndex(
                name: "IX_Hires_HiringCompanyId",
                table: "Hires");

            migrationBuilder.DropColumn(
                name: "HiringCompanyId",
                table: "Hires");
        }
    }
}

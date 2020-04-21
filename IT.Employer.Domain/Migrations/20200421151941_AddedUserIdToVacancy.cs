using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IT.Employer.Domain.Migrations
{
    public partial class AddedUserIdToVacancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Vacancies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_UserId",
                table: "Vacancies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_AspNetUsers_UserId",
                table: "Vacancies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_AspNetUsers_UserId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_UserId",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vacancies");
        }
    }
}

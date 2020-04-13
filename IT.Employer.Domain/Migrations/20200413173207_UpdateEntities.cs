using Microsoft.EntityFrameworkCore.Migrations;

namespace IT.Employer.Domain.Migrations
{
    public partial class UpdateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Vacancies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Vacancies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Companies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Companies");
        }
    }
}

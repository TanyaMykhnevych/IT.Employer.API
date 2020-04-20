using Microsoft.EntityFrameworkCore.Migrations;

namespace IT.Employer.Domain.Migrations
{
    public partial class AddedVacancyImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Vacancies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Vacancies");
        }
    }
}

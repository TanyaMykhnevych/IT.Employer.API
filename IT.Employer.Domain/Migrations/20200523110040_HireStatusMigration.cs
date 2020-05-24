using Microsoft.EntityFrameworkCore.Migrations;

namespace IT.Employer.Domain.Migrations
{
    public partial class HireStatusMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Hires");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Hires",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Hires");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Hires",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

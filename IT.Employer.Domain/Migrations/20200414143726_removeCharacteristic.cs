using Microsoft.EntityFrameworkCore.Migrations;

namespace IT.Employer.Domain.Migrations
{
    public partial class removeCharacteristic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Сharacteristic",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Сharacteristic",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

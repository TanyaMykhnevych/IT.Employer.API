using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IT.Employer.Domain.Migrations
{
    public partial class EmployeeAdjustment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableUntil",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "HourRate",
                table: "Employees",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Inactive",
                table: "Employees",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableUntil",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "HourRate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Inactive",
                table: "Employees");
        }
    }
}

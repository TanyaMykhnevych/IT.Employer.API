using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IT.Employer.Domain.Migrations
{
    public partial class HireMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hires",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    HiredFrom = table.Column<DateTime>(nullable: false),
                    HiredTo = table.Column<DateTime>(nullable: false),
                    TotalHiringRate = table.Column<decimal>(nullable: false),
                    HireMessage = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    ApproveDate = table.Column<DateTime>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    TeamId = table.Column<Guid>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hires_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hires_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hires_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hires_CompanyId",
                table: "Hires",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Hires_EmployeeId",
                table: "Hires",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hires_TeamId",
                table: "Hires",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hires");
        }
    }
}

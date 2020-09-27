using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class addcollumnStartEndTime_EmployeeWorking_20201407 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "EmployeeWorkingExperiences",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "EmployeeWorkingExperiences",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "EmployeeWorkingExperiences");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "EmployeeWorkingExperiences");
        }
    }
}

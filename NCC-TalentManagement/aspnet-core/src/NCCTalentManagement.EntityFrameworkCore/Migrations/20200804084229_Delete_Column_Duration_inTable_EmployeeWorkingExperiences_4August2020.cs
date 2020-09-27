using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class Delete_Column_Duration_inTable_EmployeeWorkingExperiences_4August2020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "EmployeeWorkingExperiences");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "EmployeeWorkingExperiences",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}

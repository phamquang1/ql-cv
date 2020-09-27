using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class _20201307_AddCollumnLevel_CVSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "CVSkills",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "CVSkills");
        }
    }
}

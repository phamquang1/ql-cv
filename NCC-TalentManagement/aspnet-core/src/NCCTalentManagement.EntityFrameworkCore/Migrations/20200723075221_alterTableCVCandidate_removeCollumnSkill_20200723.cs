using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class alterTableCVCandidate_removeCollumnSkill_20200723 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Skill",
                table: "CVCandidates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Skill",
                table: "CVCandidates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

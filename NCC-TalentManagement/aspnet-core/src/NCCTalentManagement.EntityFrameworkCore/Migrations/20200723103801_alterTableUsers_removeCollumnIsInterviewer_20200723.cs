using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class alterTableUsers_removeCollumnIsInterviewer_20200723 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInterviewer",
                table: "AbpUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInterviewer",
                table: "AbpUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

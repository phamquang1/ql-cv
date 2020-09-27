using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class altertableUser_addCollumnIsInterviewer_20200722 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInterviewer",
                table: "AbpUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInterviewer",
                table: "AbpUsers");
        }
    }
}

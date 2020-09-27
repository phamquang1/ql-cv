using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class AddCollumnPersonalAttribute_tableUser_20200715 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonalAttribute",
                table: "AbpUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalAttribute",
                table: "AbpUsers");
        }
    }
}

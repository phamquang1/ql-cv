using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class _20201407_AddEmailAddressInCV_AbpUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddressInCV",
                table: "AbpUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddressInCV",
                table: "AbpUsers");
        }
    }
}

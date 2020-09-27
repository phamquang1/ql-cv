using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class _20201407_AddBrach_AbpUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_BranchId",
                table: "AbpUsers",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Branch_BranchId",
                table: "AbpUsers",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Branch_BranchId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_BranchId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "AbpUsers");
        }
    }
}

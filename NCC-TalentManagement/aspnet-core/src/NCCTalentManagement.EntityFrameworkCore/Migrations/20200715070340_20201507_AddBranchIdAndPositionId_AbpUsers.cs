using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class _20201507_AddBranchIdAndPositionId_AbpUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Branch_BranchId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_PositionType_PositionId",
                table: "AbpUsers");

            migrationBuilder.AlterColumn<long>(
                name: "PositionId",
                table: "AbpUsers",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BranchId",
                table: "AbpUsers",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Branch_BranchId",
                table: "AbpUsers",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_PositionType_PositionId",
                table: "AbpUsers",
                column: "PositionId",
                principalTable: "PositionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Branch_BranchId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_PositionType_PositionId",
                table: "AbpUsers");

            migrationBuilder.AlterColumn<long>(
                name: "PositionId",
                table: "AbpUsers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "BranchId",
                table: "AbpUsers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Branch_BranchId",
                table: "AbpUsers",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_PositionType_PositionId",
                table: "AbpUsers",
                column: "PositionId",
                principalTable: "PositionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

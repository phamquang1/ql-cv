using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class altercollumnUserIdNotNull_EmployeeWorking_20201407 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeWorkingExperiences_AbpUsers",
                table: "EmployeeWorkingExperiences");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "EmployeeWorkingExperiences",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeWorkingExperiences_AbpUsers",
                table: "EmployeeWorkingExperiences",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeWorkingExperiences_AbpUsers",
                table: "EmployeeWorkingExperiences");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "EmployeeWorkingExperiences",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeWorkingExperiences_AbpUsers",
                table: "EmployeeWorkingExperiences",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

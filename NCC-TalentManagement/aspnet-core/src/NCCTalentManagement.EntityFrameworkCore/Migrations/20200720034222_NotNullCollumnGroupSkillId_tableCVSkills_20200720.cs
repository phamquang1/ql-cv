using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class NotNullCollumnGroupSkillId_tableCVSkills_20200720 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CVSkills_GroupSkills",
                table: "CVSkills");

            migrationBuilder.AlterColumn<long>(
                name: "GroupSkillId",
                table: "CVSkills",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CVSkills_GroupSkills",
                table: "CVSkills",
                column: "GroupSkillId",
                principalTable: "GroupSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CVSkills_GroupSkills",
                table: "CVSkills");

            migrationBuilder.AlterColumn<long>(
                name: "GroupSkillId",
                table: "CVSkills",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_CVSkills_GroupSkills",
                table: "CVSkills",
                column: "GroupSkillId",
                principalTable: "GroupSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

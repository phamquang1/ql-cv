using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class alterTableCVSkill_allowNullCollumnGroupSkillId_20200723 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CVSkills_GroupSkills",
                table: "CVSkills");

            migrationBuilder.AlterColumn<long>(
                name: "GroupSkillId",
                table: "CVSkills",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_CVSkills_GroupSkills",
                table: "CVSkills",
                column: "GroupSkillId",
                principalTable: "GroupSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CVSkills_GroupSkills",
                table: "CVSkills",
                column: "GroupSkillId",
                principalTable: "GroupSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

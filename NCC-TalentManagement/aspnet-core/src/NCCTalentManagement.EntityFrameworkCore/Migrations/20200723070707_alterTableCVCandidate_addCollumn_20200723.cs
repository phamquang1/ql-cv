using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class alterTableCVCandidate_addCollumn_20200723 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PresenterId",
                table: "CVCandidates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Skill",
                table: "CVCandidates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkExperience",
                table: "CVCandidates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CVCandidates_PresenterId",
                table: "CVCandidates",
                column: "PresenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CVCandidates_AbpUsers_PresenterId",
                table: "CVCandidates",
                column: "PresenterId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CVCandidates_AbpUsers_PresenterId",
                table: "CVCandidates");

            migrationBuilder.DropIndex(
                name: "IX_CVCandidates_PresenterId",
                table: "CVCandidates");

            migrationBuilder.DropColumn(
                name: "PresenterId",
                table: "CVCandidates");

            migrationBuilder.DropColumn(
                name: "Skill",
                table: "CVCandidates");

            migrationBuilder.DropColumn(
                name: "WorkExperience",
                table: "CVCandidates");
        }
    }
}

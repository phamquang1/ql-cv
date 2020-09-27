using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class alterTableCVCandidate_removeEducationAndRelationshipEducation_20200723 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_CVCandidates",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_CVCandidateId",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "CVCandidateId",
                table: "Educations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CVCandidateId",
                table: "Educations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_CVCandidateId",
                table: "Educations",
                column: "CVCandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_CVCandidates",
                table: "Educations",
                column: "CVCandidateId",
                principalTable: "CVCandidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

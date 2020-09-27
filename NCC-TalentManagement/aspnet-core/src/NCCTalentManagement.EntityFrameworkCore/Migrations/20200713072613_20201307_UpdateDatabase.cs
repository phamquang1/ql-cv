using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NCCTalentManagement.Migrations
{
    public partial class _20201307_UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PositionCVs");

            migrationBuilder.DropTable(
                name: "ApplyPositionType");

            migrationBuilder.AddColumn<long>(
                name: "PositionId",
                table: "CVCandidates",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PositionId",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PositionType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CVCandidates_PositionId",
                table: "CVCandidates",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_PositionId",
                table: "AbpUsers",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_PositionType_PositionId",
                table: "AbpUsers",
                column: "PositionId",
                principalTable: "PositionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CVCandidates_PositionType",
                table: "CVCandidates",
                column: "PositionId",
                principalTable: "PositionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_PositionType_PositionId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CVCandidates_PositionType",
                table: "CVCandidates");

            migrationBuilder.DropTable(
                name: "PositionType");

            migrationBuilder.DropIndex(
                name: "IX_CVCandidates_PositionId",
                table: "CVCandidates");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_PositionId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "CVCandidates");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "AbpUsers");

            migrationBuilder.CreateTable(
                name: "ApplyPositionType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyPositionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PositionCVs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplyPositionId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    CVCandidateId = table.Column<long>(type: "bigint", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionCVs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PositionCVs_ApplyPositionType",
                        column: x => x.ApplyPositionId,
                        principalTable: "ApplyPositionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Positions_CVCandidates",
                        column: x => x.CVCandidateId,
                        principalTable: "CVCandidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PositionCVs_ApplyPositionId",
                table: "PositionCVs",
                column: "ApplyPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionCVs_CVCandidateId",
                table: "PositionCVs",
                column: "CVCandidateId");
        }
    }
}

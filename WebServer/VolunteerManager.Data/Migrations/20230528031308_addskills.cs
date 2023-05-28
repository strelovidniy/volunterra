using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addskills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationRequestSkill",
                columns: table => new
                {
                    OrganizationRequestsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationRequestSkill", x => new { x.OrganizationRequestsId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_OrganizationRequestSkill_OrganizationRequests_OrganizationRequestsId",
                        column: x => x.OrganizationRequestsId,
                        principalSchema: "dbo",
                        principalTable: "OrganizationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationRequestSkill_Skill_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationRequestSkill_SkillsId",
                table: "OrganizationRequestSkill",
                column: "SkillsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationRequestSkill");

            migrationBuilder.DropTable(
                name: "Skill");
        }
    }
}

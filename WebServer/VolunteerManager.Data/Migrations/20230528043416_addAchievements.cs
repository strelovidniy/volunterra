using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addAchievements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Seniority = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievement_OrganizationRequests_OrganizationRequestId",
                        column: x => x.OrganizationRequestId,
                        principalSchema: "dbo",
                        principalTable: "OrganizationRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Achievement_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_OrganizationRequestId",
                table: "Achievement",
                column: "OrganizationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_UserId",
                table: "Achievement",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievement");
        }
    }
}

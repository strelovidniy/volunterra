using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrgRequestConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationRequestReply",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationRequestReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationRequestReply_OrganizationRequests_OrganizationRequestId",
                        column: x => x.OrganizationRequestId,
                        principalSchema: "dbo",
                        principalTable: "OrganizationRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationRequestReply_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationRequestReply_OrganizationRequestId",
                table: "OrganizationRequestReply",
                column: "OrganizationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationRequestReply_UserId",
                table: "OrganizationRequestReply",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationRequestReply");
        }
    }
}

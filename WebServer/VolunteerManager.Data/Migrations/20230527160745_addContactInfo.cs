using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addContactInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationRequests_Requests_RequestId",
                schema: "dbo",
                table: "OrganizationRequests");

            migrationBuilder.DropTable(
                name: "Requests",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationRequests_RequestId",
                schema: "dbo",
                table: "OrganizationRequests");

            migrationBuilder.DropColumn(
                name: "ServedCount",
                schema: "dbo",
                table: "OrganizationRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactInfoId",
                schema: "dbo",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactInfoId",
                schema: "dbo",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "OrganizationRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "dbo",
                table: "OrganizationRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "OrganizationRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInfo_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "dbo",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactInfo_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_OrganizationId",
                table: "ContactInfo",
                column: "OrganizationId",
                unique: true,
                filter: "[OrganizationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_UserId",
                table: "ContactInfo",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInfo");

            migrationBuilder.DropColumn(
                name: "ContactInfoId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContactInfoId",
                schema: "dbo",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "dbo",
                table: "OrganizationRequests");

            migrationBuilder.DropColumn(
                name: "Location",
                schema: "dbo",
                table: "OrganizationRequests");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "dbo",
                table: "OrganizationRequests");

            migrationBuilder.AddColumn<decimal>(
                name: "ServedCount",
                schema: "dbo",
                table: "OrganizationRequests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Requests",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ImageDataUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationRequests_RequestId",
                schema: "dbo",
                table: "OrganizationRequests",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CreatedById",
                schema: "dbo",
                table: "Requests",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationRequests_Requests_RequestId",
                schema: "dbo",
                table: "OrganizationRequests",
                column: "RequestId",
                principalSchema: "dbo",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrganizationRequestInvocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInfo_Organizations_OrganizationId",
                schema: "dbo",
                table: "ContactInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactInfo_Users_UserId",
                schema: "dbo",
                table: "ContactInfo");

            migrationBuilder.DropIndex(
                name: "IX_ContactInfo_OrganizationId",
                schema: "dbo",
                table: "ContactInfo");

            migrationBuilder.DropIndex(
                name: "IX_ContactInfo_UserId",
                schema: "dbo",
                table: "ContactInfo");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "dbo",
                table: "ContactInfo");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "ContactInfo");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationRequestCategory",
                schema: "dbo",
                table: "OrganizationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInfo_Organizations_Id",
                schema: "dbo",
                table: "ContactInfo",
                column: "Id",
                principalSchema: "dbo",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInfo_Users_Id",
                schema: "dbo",
                table: "ContactInfo",
                column: "Id",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInfo_Organizations_Id",
                schema: "dbo",
                table: "ContactInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactInfo_Users_Id",
                schema: "dbo",
                table: "ContactInfo");

            migrationBuilder.DropColumn(
                name: "OrganizationRequestCategory",
                schema: "dbo",
                table: "OrganizationRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "dbo",
                table: "ContactInfo",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "dbo",
                table: "ContactInfo",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_OrganizationId",
                schema: "dbo",
                table: "ContactInfo",
                column: "OrganizationId",
                unique: true,
                filter: "[OrganizationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_UserId",
                schema: "dbo",
                table: "ContactInfo",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInfo_Organizations_OrganizationId",
                schema: "dbo",
                table: "ContactInfo",
                column: "OrganizationId",
                principalSchema: "dbo",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInfo_Users_UserId",
                schema: "dbo",
                table: "ContactInfo",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

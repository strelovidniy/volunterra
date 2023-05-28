using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class Sleeping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInfo_Organizations_Id",
                schema: "dbo",
                table: "ContactInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactInfo_Users_Id",
                schema: "dbo",
                table: "ContactInfo");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "dbo",
                table: "ContactInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "dbo",
                table: "ContactInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_OrganizationId",
                schema: "dbo",
                table: "ContactInfo",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_UserId",
                schema: "dbo",
                table: "ContactInfo",
                column: "UserId",
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDepositDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompoundingFrequency",
                schema: "dbo",
                table: "Deposits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfYears",
                schema: "dbo",
                table: "Deposits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RateOfInterest",
                schema: "dbo",
                table: "Deposits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompoundingFrequency",
                schema: "dbo",
                table: "Deposits");
            
            migrationBuilder.DropColumn(
                name: "NumberOfYears",
                schema: "dbo",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "RateOfInterest",
                schema: "dbo",
                table: "Deposits");
        }
    }
}

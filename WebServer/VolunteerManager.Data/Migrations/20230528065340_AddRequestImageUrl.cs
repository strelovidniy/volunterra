using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageDataUrl",
                schema: "dbo",
                table: "OrganizationRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageDataUrl",
                schema: "dbo",
                table: "OrganizationRequests");
        }
    }
}

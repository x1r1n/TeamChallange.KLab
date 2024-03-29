using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedApplicationUser_DeleteProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLocalPath",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageLocalPath",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

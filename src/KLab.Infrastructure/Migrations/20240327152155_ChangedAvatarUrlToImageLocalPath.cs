using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAvatarUrlToImageLocalPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvatarUrl",
                table: "Users",
                newName: "ImageLocalPath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageLocalPath",
                table: "Users",
                newName: "AvatarUrl");
        }
    }
}

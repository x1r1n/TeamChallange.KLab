using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Users",
                newName: "Nickname");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "Users",
                newName: "FullName");
        }
    }
}

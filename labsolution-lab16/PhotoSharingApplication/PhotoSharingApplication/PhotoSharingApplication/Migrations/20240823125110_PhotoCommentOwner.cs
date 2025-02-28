using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoSharingApplication.Migrations.PhotosDb
{
    /// <inheritdoc />
    public partial class PhotoCommentOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Comments");
        }
    }
}

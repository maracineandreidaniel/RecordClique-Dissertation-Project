using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordClique_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changenamesreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Reviews",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "Stars",
                table: "Reviews",
                newName: "Rating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Reviews",
                newName: "Stars");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Reviews",
                newName: "Text");
        }
    }
}

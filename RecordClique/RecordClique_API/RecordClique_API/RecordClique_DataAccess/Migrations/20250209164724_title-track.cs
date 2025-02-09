using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordClique_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class titletrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Tracks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Tracks");
        }
    }
}

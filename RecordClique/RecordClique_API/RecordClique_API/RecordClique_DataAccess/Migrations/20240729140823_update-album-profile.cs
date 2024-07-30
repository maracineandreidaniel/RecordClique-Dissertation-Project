using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordClique_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatealbumprofile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumArtistLink_Albums_FK_AlbumId",
                table: "AlbumArtistLink");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumArtistLink_Artists_FK_ArtistId",
                table: "AlbumArtistLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlbumArtistLink",
                table: "AlbumArtistLink");

            migrationBuilder.RenameTable(
                name: "AlbumArtistLink",
                newName: "AlbumArtistLinks");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumArtistLink_FK_ArtistId",
                table: "AlbumArtistLinks",
                newName: "IX_AlbumArtistLinks_FK_ArtistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlbumArtistLinks",
                table: "AlbumArtistLinks",
                columns: new[] { "FK_AlbumId", "FK_ArtistId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumArtistLinks_Albums_FK_AlbumId",
                table: "AlbumArtistLinks",
                column: "FK_AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumArtistLinks_Artists_FK_ArtistId",
                table: "AlbumArtistLinks",
                column: "FK_ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumArtistLinks_Albums_FK_AlbumId",
                table: "AlbumArtistLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumArtistLinks_Artists_FK_ArtistId",
                table: "AlbumArtistLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlbumArtistLinks",
                table: "AlbumArtistLinks");

            migrationBuilder.RenameTable(
                name: "AlbumArtistLinks",
                newName: "AlbumArtistLink");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumArtistLinks_FK_ArtistId",
                table: "AlbumArtistLink",
                newName: "IX_AlbumArtistLink_FK_ArtistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlbumArtistLink",
                table: "AlbumArtistLink",
                columns: new[] { "FK_AlbumId", "FK_ArtistId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumArtistLink_Albums_FK_AlbumId",
                table: "AlbumArtistLink",
                column: "FK_AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumArtistLink_Artists_FK_ArtistId",
                table: "AlbumArtistLink",
                column: "FK_ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

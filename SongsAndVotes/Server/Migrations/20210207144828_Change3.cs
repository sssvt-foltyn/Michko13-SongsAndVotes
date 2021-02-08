using Microsoft.EntityFrameworkCore.Migrations;

namespace SongsAndVotes.Server.Migrations
{
    public partial class Change3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artists_ArtistByID",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Users_UserByID",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Albums_AlbumID",
                table: "Songs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Albums",
                table: "Albums");

            migrationBuilder.RenameTable(
                name: "Albums",
                newName: "Playlists");

            migrationBuilder.RenameColumn(
                name: "AlbumID",
                table: "Songs",
                newName: "PlaylistID");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_AlbumID",
                table: "Songs",
                newName: "IX_Songs_PlaylistID");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_UserByID",
                table: "Playlists",
                newName: "IX_Playlists_UserByID");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_ArtistByID",
                table: "Playlists",
                newName: "IX_Playlists_ArtistByID");

            migrationBuilder.AddColumn<int>(
                name: "ArtistID",
                table: "Songs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Playlists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ArtistID",
                table: "Songs",
                column: "ArtistID");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Artists_ArtistByID",
                table: "Playlists",
                column: "ArtistByID",
                principalTable: "Artists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_UserByID",
                table: "Playlists",
                column: "UserByID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Artists_ArtistID",
                table: "Songs",
                column: "ArtistID",
                principalTable: "Artists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Playlists_PlaylistID",
                table: "Songs",
                column: "PlaylistID",
                principalTable: "Playlists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Artists_ArtistByID",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_UserByID",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Artists_ArtistID",
                table: "Songs");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Playlists_PlaylistID",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_ArtistID",
                table: "Songs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "ArtistID",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Playlists");

            migrationBuilder.RenameTable(
                name: "Playlists",
                newName: "Albums");

            migrationBuilder.RenameColumn(
                name: "PlaylistID",
                table: "Songs",
                newName: "AlbumID");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_PlaylistID",
                table: "Songs",
                newName: "IX_Songs_AlbumID");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_UserByID",
                table: "Albums",
                newName: "IX_Albums_UserByID");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_ArtistByID",
                table: "Albums",
                newName: "IX_Albums_ArtistByID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Albums",
                table: "Albums",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "ArtistsSongs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistID = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    SongsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistsSongs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ArtistsSongs_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistsSongs_Songs_SongsID",
                        column: x => x.SongsID,
                        principalTable: "Songs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistsSongs_ArtistID",
                table: "ArtistsSongs",
                column: "ArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistsSongs_SongsID",
                table: "ArtistsSongs",
                column: "SongsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artists_ArtistByID",
                table: "Albums",
                column: "ArtistByID",
                principalTable: "Artists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Users_UserByID",
                table: "Albums",
                column: "UserByID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Albums_AlbumID",
                table: "Songs",
                column: "AlbumID",
                principalTable: "Albums",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

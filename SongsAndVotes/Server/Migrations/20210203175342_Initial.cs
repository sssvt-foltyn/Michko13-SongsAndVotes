using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SongsAndVotes.Server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistByID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserByID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Albums_Artists_ArtistByID",
                        column: x => x.ArtistByID,
                        principalTable: "Artists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Albums_Users_UserByID",
                        column: x => x.UserByID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserUploadedID = table.Column<int>(type: "int", nullable: true),
                    ArtistID = table.Column<int>(type: "int", nullable: true),
                    AlbumID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Songs_Albums_AlbumID",
                        column: x => x.AlbumID,
                        principalTable: "Albums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Songs_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Songs_Users_UserUploadedID",
                        column: x => x.UserUploadedID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArtistsSongs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistID = table.Column<int>(type: "int", nullable: false),
                    SongsID = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_Albums_ArtistByID",
                table: "Albums",
                column: "ArtistByID");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_UserByID",
                table: "Albums",
                column: "UserByID");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistsSongs_ArtistID",
                table: "ArtistsSongs",
                column: "ArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistsSongs_SongsID",
                table: "ArtistsSongs",
                column: "SongsID");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_AlbumID",
                table: "Songs",
                column: "AlbumID");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ArtistID",
                table: "Songs",
                column: "ArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_UserUploadedID",
                table: "Songs",
                column: "UserUploadedID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistsSongs");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

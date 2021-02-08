using Microsoft.EntityFrameworkCore.Migrations;

namespace SongsAndVotes.Server.Migrations
{
    public partial class change4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Users_UserUploadedID",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "UserUploadedID",
                table: "Songs",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_UserUploadedID",
                table: "Songs",
                newName: "IX_Songs_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Users_UserID",
                table: "Songs",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Users_UserID",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Songs",
                newName: "UserUploadedID");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_UserID",
                table: "Songs",
                newName: "IX_Songs_UserUploadedID");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Users_UserUploadedID",
                table: "Songs",
                column: "UserUploadedID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncrediSpots.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class CommentsUpdate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_spots_SpotModelId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_users_UserModelId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SpotModelId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserModelId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SpotModelId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Comments");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SpotId",
                table: "Comments",
                column: "SpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_spots_SpotId",
                table: "Comments",
                column: "SpotId",
                principalTable: "spots",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_users_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_spots_SpotId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_users_AuthorId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SpotId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "SpotModelId",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SpotModelId",
                table: "Comments",
                column: "SpotModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserModelId",
                table: "Comments",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_spots_SpotModelId",
                table: "Comments",
                column: "SpotModelId",
                principalTable: "spots",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_users_UserModelId",
                table: "Comments",
                column: "UserModelId",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}

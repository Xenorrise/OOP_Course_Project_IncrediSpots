using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncrediSpots.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class CommentsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserLectureBookRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Lectures",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Lectures",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Lectures",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_BookId",
                table: "Lectures",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_UserId",
                table: "Lectures",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Books_BookId",
                table: "Lectures",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Users_UserId",
                table: "Lectures",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Books_BookId",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Users_UserId",
                table: "Lectures");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_BookId",
                table: "Lectures");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_UserId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Lectures");
        }
    }
}

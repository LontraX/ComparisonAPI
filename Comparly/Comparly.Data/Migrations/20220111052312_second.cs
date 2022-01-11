using Microsoft.EntityFrameworkCore.Migrations;

namespace Comparly.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "submissions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_submissions_AppUserId",
                table: "submissions",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_submissions_AspNetUsers_AppUserId",
                table: "submissions",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_submissions_AspNetUsers_AppUserId",
                table: "submissions");

            migrationBuilder.DropIndex(
                name: "IX_submissions_AppUserId",
                table: "submissions");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "submissions");
        }
    }
}

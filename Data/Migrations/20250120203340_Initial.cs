using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTr.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WorkStat",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WorkSession",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Task",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkStat_UserId",
                table: "WorkStat",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSession_UserId",
                table: "WorkSession",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_UserId",
                table: "Task",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_AspNetUsers_UserId",
                table: "Task",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSession_AspNetUsers_UserId",
                table: "WorkSession",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkStat_AspNetUsers_UserId",
                table: "WorkStat",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_AspNetUsers_UserId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSession_AspNetUsers_UserId",
                table: "WorkSession");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkStat_AspNetUsers_UserId",
                table: "WorkStat");

            migrationBuilder.DropIndex(
                name: "IX_WorkStat_UserId",
                table: "WorkStat");

            migrationBuilder.DropIndex(
                name: "IX_WorkSession_UserId",
                table: "WorkSession");

            migrationBuilder.DropIndex(
                name: "IX_Task_UserId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkStat");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkSession");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Task");
        }
    }
}

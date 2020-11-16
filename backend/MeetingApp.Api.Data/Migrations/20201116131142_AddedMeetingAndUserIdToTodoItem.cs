using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingApp.Api.Data.Migrations
{
    public partial class AddedMeetingAndUserIdToTodoItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_Meetings_MeetingId",
                table: "TodoItems");

            migrationBuilder.AlterColumn<int>(
                name: "MeetingId",
                table: "TodoItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_Meetings_MeetingId",
                table: "TodoItems",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_Meetings_MeetingId",
                table: "TodoItems");

            migrationBuilder.AlterColumn<int>(
                name: "MeetingId",
                table: "TodoItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_Meetings_MeetingId",
                table: "TodoItems",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

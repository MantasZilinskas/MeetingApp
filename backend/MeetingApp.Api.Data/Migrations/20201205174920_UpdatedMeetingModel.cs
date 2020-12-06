using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingApp.Api.Data.Migrations
{
    public partial class UpdatedMeetingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextEditorData",
                table: "Meetings",
                type: "varchar(MAX)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextEditorData",
                table: "Meetings");
        }
    }
}

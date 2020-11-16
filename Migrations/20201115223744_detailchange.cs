using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressBook.Migrations
{
    public partial class detailchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressUserId",
                table: "Profile",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profile_AddressUserId",
                table: "Profile",
                column: "AddressUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_AspNetUsers_AddressUserId",
                table: "Profile",
                column: "AddressUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_AspNetUsers_AddressUserId",
                table: "Profile");

            migrationBuilder.DropIndex(
                name: "IX_Profile_AddressUserId",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "AddressUserId",
                table: "Profile");
        }
    }
}

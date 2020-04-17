using Microsoft.EntityFrameworkCore.Migrations;

namespace Health.API.Migrations
{
    public partial class Usersv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fever_User_UserId",
                table: "Fever");

            migrationBuilder.DropForeignKey(
                name: "FK_Temperature_User_UserId",
                table: "Temperature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Temperature",
                table: "Temperature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fever",
                table: "Fever");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Temperature",
                newName: "Temperatures");

            migrationBuilder.RenameTable(
                name: "Fever",
                newName: "Fevers");

            migrationBuilder.RenameIndex(
                name: "IX_Temperature_UserId",
                table: "Temperatures",
                newName: "IX_Temperatures_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Fever_UserId",
                table: "Fevers",
                newName: "IX_Fevers_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Temperatures",
                table: "Temperatures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fevers",
                table: "Fevers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fevers_Users_UserId",
                table: "Fevers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Temperatures_Users_UserId",
                table: "Temperatures",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fevers_Users_UserId",
                table: "Fevers");

            migrationBuilder.DropForeignKey(
                name: "FK_Temperatures_Users_UserId",
                table: "Temperatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Temperatures",
                table: "Temperatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fevers",
                table: "Fevers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Temperatures",
                newName: "Temperature");

            migrationBuilder.RenameTable(
                name: "Fevers",
                newName: "Fever");

            migrationBuilder.RenameIndex(
                name: "IX_Temperatures_UserId",
                table: "Temperature",
                newName: "IX_Temperature_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Fevers_UserId",
                table: "Fever",
                newName: "IX_Fever_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Temperature",
                table: "Temperature",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fever",
                table: "Fever",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fever_User_UserId",
                table: "Fever",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Temperature_User_UserId",
                table: "Temperature",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

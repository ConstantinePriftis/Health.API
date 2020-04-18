using Microsoft.EntityFrameworkCore.Migrations;

namespace Health.API.Migrations
{
    public partial class RenewedTemperature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasFever",
                table: "Temperatures",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasFever",
                table: "Temperatures");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMySpotAPI.Migrations
{
    public partial class Karticnoplacanje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "karticnoPlacanje",
                table: "Usluga",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "karticnoPlacanje",
                table: "Usluga");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMySpotAPI.Migrations
{
    public partial class Smjestaj6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "datumIseljenja",
                table: "Usluga");

            migrationBuilder.DropColumn(
                name: "datumUseljenja",
                table: "Usluga");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "datumIseljenja",
                table: "Usluga",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datumUseljenja",
                table: "Usluga",
                type: "datetime2",
                nullable: true);
        }
    }
}

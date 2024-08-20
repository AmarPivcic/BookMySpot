using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMySpotAPI.Migrations
{
    public partial class Smjestaj4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "datumIseljenja",
                table: "Usluga");

            migrationBuilder.DropColumn(
                name: "datumUseljenja",
                table: "Usluga");
        }
    }
}

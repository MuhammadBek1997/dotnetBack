using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FudballManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StadiumLocations");

            migrationBuilder.DropColumn(
                name: "StadiumLocationId",
                table: "Stadiums");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Stadiums",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Stadiums",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Stadiums");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Stadiums");

            migrationBuilder.AddColumn<long>(
                name: "StadiumLocationId",
                table: "Stadiums",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "StadiumLocations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    PlaceName = table.Column<string>(type: "text", nullable: false),
                    StadiumId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StadiumLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StadiumLocations_Stadiums_Id",
                        column: x => x.Id,
                        principalTable: "Stadiums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}

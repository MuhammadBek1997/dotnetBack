using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FudballManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class smth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvageRating",
                table: "stadiumRatings");

            migrationBuilder.DropColumn(
                name: "TotalVotes",
                table: "stadiumRatings");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "stadiumRatings",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "stadiumRatings",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<double>(
                name: "AvageRating",
                table: "stadiumRatings",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TotalVotes",
                table: "stadiumRatings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

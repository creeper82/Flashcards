using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flashcards.Migrations
{
    /// <inheritdoc />
    public partial class AddCreationTimestamptoDeckandCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTimestamp",
                table: "Decks",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTimestamp",
                table: "Cards",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTimestamp",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "CreationTimestamp",
                table: "Cards");
        }
    }
}

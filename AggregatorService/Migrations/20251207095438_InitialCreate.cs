using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AggregatorService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaderboardSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DatePulled = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderboardSnapshots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaderboardEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterName = table.Column<string>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Rank = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Played = table.Column<int>(type: "INTEGER", nullable: false),
                    Won = table.Column<int>(type: "INTEGER", nullable: false),
                    Lost = table.Column<int>(type: "INTEGER", nullable: false),
                    SnapshotId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderboardEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaderboardEntry_LeaderboardSnapshots_SnapshotId",
                        column: x => x.SnapshotId,
                        principalTable: "LeaderboardSnapshots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardEntry_SnapshotId",
                table: "LeaderboardEntry",
                column: "SnapshotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaderboardEntry");

            migrationBuilder.DropTable(
                name: "LeaderboardSnapshots");
        }
    }
}

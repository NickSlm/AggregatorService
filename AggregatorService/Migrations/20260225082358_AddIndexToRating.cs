using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AggregatorService.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardEntry_Rank_Rating",
                table: "LeaderboardEntry",
                columns: new[] { "Rank", "Rating" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LeaderboardEntry_Rank_Rating",
                table: "LeaderboardEntry");
        }
    }
}

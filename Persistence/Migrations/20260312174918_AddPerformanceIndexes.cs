using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPerformanceIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserFollowings_ObserverId",
                table: "UserFollowings",
                column: "ObserverId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityAttendees_IsHost",
                table: "ActivityAttendees",
                column: "IsHost");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFollowings_ObserverId",
                table: "UserFollowings");

            migrationBuilder.DropIndex(
                name: "IX_ActivityAttendees_IsHost",
                table: "ActivityAttendees");
        }
    }
}

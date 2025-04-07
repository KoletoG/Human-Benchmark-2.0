using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Human_Benchmark_2._0.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReactionTimeToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reactionTimesArray",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reactionTimesArray",
                table: "AspNetUsers");
        }
    }
}

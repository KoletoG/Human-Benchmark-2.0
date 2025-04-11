using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Human_Benchmark_2._0.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReverseWordAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reverseWordsScoreArray",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reverseWordsScoreArray",
                table: "AspNetUsers");
        }
    }
}

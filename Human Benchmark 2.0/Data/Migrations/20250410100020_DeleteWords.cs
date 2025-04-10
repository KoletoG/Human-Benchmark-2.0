using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Human_Benchmark_2._0.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteWords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wordDataModels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wordDataModels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wordDataModels", x => x.Id);
                });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fotoservice.Migrations
{
    /// <inheritdoc />
    public partial class categoryChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainPhoto",
                table: "Categories",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainPhoto",
                table: "Categories");
        }
    }
}

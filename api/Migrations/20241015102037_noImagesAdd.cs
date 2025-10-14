using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fotoservice.Migrations
{
    /// <inheritdoc />
    public partial class noImagesAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number_of_images",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number_of_images",
                table: "Categories");
        }
    }
}

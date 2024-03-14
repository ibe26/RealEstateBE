using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateDataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addednewrowtoproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OnListing",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnListing",
                table: "Properties");
        }
    }
}

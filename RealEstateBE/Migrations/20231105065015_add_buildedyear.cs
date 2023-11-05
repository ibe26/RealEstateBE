using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateBE.Migrations
{
    /// <inheritdoc />
    public partial class add_buildedyear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "BuildedYear",
                table: "Properties",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildedYear",
                table: "Properties");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateBE.Migrations
{
    /// <inheritdoc />
    public partial class add_property_collection_to_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserID",
                table: "Properties",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_UserID",
                table: "Properties",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_UserID",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_UserID",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Properties");
        }
    }
}

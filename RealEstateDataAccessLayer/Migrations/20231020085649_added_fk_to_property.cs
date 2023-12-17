using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateDataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class added_fk_to_property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PropertyType",
                table: "Properties",
                newName: "PropertyTypeID");

            migrationBuilder.RenameColumn(
                name: "PropertyListingType",
                table: "Properties",
                newName: "PropertyListingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyListingTypeID",
                table: "Properties",
                column: "PropertyListingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeID",
                table: "Properties",
                column: "PropertyTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyListingTypes_PropertyListingTypeID",
                table: "Properties",
                column: "PropertyListingTypeID",
                principalTable: "PropertyListingTypes",
                principalColumn: "PropertyListingTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeID",
                table: "Properties",
                column: "PropertyTypeID",
                principalTable: "PropertyTypes",
                principalColumn: "PropertyTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyListingTypes_PropertyListingTypeID",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeID",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_PropertyListingTypeID",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_PropertyTypeID",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "PropertyTypeID",
                table: "Properties",
                newName: "PropertyType");

            migrationBuilder.RenameColumn(
                name: "PropertyListingTypeID",
                table: "Properties",
                newName: "PropertyListingType");
        }
    }
}

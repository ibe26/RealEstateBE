using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateDataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addedguid3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_PropertyListingTypes_PropertyListingTypeID",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_PropertyTypes_PropertyTypeID",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Users_UserID",
                table: "Property");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Property",
                table: "Property");

            migrationBuilder.RenameTable(
                name: "Property",
                newName: "Properties");

            migrationBuilder.RenameIndex(
                name: "IX_Property_UserID",
                table: "Properties",
                newName: "IX_Properties_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Property_PropertyTypeID",
                table: "Properties",
                newName: "IX_Properties_PropertyTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Property_PropertyListingTypeID",
                table: "Properties",
                newName: "IX_Properties_PropertyListingTypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "PropertyID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_UserID",
                table: "Properties",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_UserID",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.RenameTable(
                name: "Properties",
                newName: "Property");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_UserID",
                table: "Property",
                newName: "IX_Property_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_PropertyTypeID",
                table: "Property",
                newName: "IX_Property_PropertyTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_PropertyListingTypeID",
                table: "Property",
                newName: "IX_Property_PropertyListingTypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Property",
                table: "Property",
                column: "PropertyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_PropertyListingTypes_PropertyListingTypeID",
                table: "Property",
                column: "PropertyListingTypeID",
                principalTable: "PropertyListingTypes",
                principalColumn: "PropertyListingTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_PropertyTypes_PropertyTypeID",
                table: "Property",
                column: "PropertyTypeID",
                principalTable: "PropertyTypes",
                principalColumn: "PropertyTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Users_UserID",
                table: "Property",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateDataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyListingTypes",
                columns: table => new
                {
                    PropertyListingTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyListingTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyListingTypes", x => x.PropertyListingTypeID);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    PropertyTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.PropertyTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "OwnedProperties",
                columns: table => new
                {
                    PropertyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyPrice = table.Column<int>(type: "int", nullable: false),
                    GrossArea = table.Column<int>(type: "int", nullable: false),
                    NetArea = table.Column<int>(type: "int", nullable: false),
                    GrossIncome = table.Column<int>(type: "int", nullable: false),
                    NetIncome = table.Column<int>(type: "int", nullable: false),
                    PropertyTypeID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnedProperties", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK_OwnedProperties_PropertyTypes_PropertyTypeID",
                        column: x => x.PropertyTypeID,
                        principalTable: "PropertyTypes",
                        principalColumn: "PropertyTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnedProperties_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateListed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyTypeID = table.Column<int>(type: "int", nullable: false),
                    PropertyListingTypeID = table.Column<int>(type: "int", nullable: false),
                    PropertyPrice = table.Column<int>(type: "int", nullable: false),
                    BedroomCount = table.Column<short>(type: "smallint", nullable: false),
                    BathroomCount = table.Column<short>(type: "smallint", nullable: false),
                    Balcony = table.Column<bool>(type: "bit", nullable: false),
                    GrossArea = table.Column<int>(type: "int", nullable: false),
                    NetArea = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quarter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dues = table.Column<int>(type: "int", nullable: false),
                    BuiltYear = table.Column<short>(type: "smallint", nullable: false),
                    OnListing = table.Column<bool>(type: "bit", nullable: false),
                    HeatSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Floor = table.Column<short>(type: "smallint", nullable: true),
                    TotalFloor = table.Column<short>(type: "smallint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyListingTypes_PropertyListingTypeID",
                        column: x => x.PropertyListingTypeID,
                        principalTable: "PropertyListingTypes",
                        principalColumn: "PropertyListingTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyTypes_PropertyTypeID",
                        column: x => x.PropertyTypeID,
                        principalTable: "PropertyTypes",
                        principalColumn: "PropertyTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OwnedProperties_PropertyTypeID",
                table: "OwnedProperties",
                column: "PropertyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedProperties_UserID",
                table: "OwnedProperties",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyListingTypeID",
                table: "Properties",
                column: "PropertyListingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeID",
                table: "Properties",
                column: "PropertyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserID",
                table: "Properties",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnedProperties");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "PropertyListingTypes");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

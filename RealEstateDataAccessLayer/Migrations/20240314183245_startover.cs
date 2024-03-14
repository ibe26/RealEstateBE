using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateDataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class startover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnedProperties");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "PropertyListingTypes");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    PropertyTypeID = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GrossArea = table.Column<int>(type: "int", nullable: false),
                    GrossIncome = table.Column<int>(type: "int", nullable: false),
                    NetArea = table.Column<int>(type: "int", nullable: false),
                    NetIncome = table.Column<int>(type: "int", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyPrice = table.Column<int>(type: "int", nullable: false),
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
                name: "Property",
                columns: table => new
                {
                    PropertyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyListingTypeID = table.Column<int>(type: "int", nullable: false),
                    PropertyTypeID = table.Column<int>(type: "int", nullable: false),
                    Balcony = table.Column<bool>(type: "bit", nullable: false),
                    BathroomCount = table.Column<short>(type: "smallint", nullable: false),
                    BedroomCount = table.Column<short>(type: "smallint", nullable: false),
                    BuiltYear = table.Column<short>(type: "smallint", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateListed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dues = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<short>(type: "smallint", nullable: true),
                    GrossArea = table.Column<int>(type: "int", nullable: false),
                    HeatSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NetArea = table.Column<int>(type: "int", nullable: false),
                    OnListing = table.Column<bool>(type: "bit", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyPrice = table.Column<int>(type: "int", nullable: false),
                    Quarter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalFloor = table.Column<short>(type: "smallint", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK_Property_PropertyListingTypes_PropertyListingTypeID",
                        column: x => x.PropertyListingTypeID,
                        principalTable: "PropertyListingTypes",
                        principalColumn: "PropertyListingTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Property_PropertyTypes_PropertyTypeID",
                        column: x => x.PropertyTypeID,
                        principalTable: "PropertyTypes",
                        principalColumn: "PropertyTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Property_Users_UserID",
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
                name: "IX_Property_PropertyListingTypeID",
                table: "Property",
                column: "PropertyListingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Property_PropertyTypeID",
                table: "Property",
                column: "PropertyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Property_UserID",
                table: "Property",
                column: "UserID");
        }
    }
}

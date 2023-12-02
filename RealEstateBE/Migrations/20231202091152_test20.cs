using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateBE.Migrations
{
    /// <inheritdoc />
    public partial class test20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Properties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyListingTypeID = table.Column<int>(type: "int", nullable: false),
                    PropertyTypeID = table.Column<int>(type: "int", nullable: false),
                    Balcony = table.Column<bool>(type: "bit", nullable: false),
                    BathroomCount = table.Column<short>(type: "smallint", nullable: false),
                    BedroomCount = table.Column<short>(type: "smallint", nullable: false),
                    BuildedYear = table.Column<short>(type: "smallint", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateListed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dues = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<short>(type: "smallint", nullable: true),
                    GrossArea = table.Column<int>(type: "int", nullable: false),
                    HeatSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NetArea = table.Column<int>(type: "int", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyPrice = table.Column<int>(type: "int", nullable: false),
                    Quarter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalFloor = table.Column<short>(type: "smallint", nullable: true)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyListingTypeID",
                table: "Properties",
                column: "PropertyListingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeID",
                table: "Properties",
                column: "PropertyTypeID");
        }
    }
}

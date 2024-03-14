using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateDataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class useraddedownedPropertiesrow5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserID",
                table: "Properties",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedProperties_PropertyTypeID",
                table: "OwnedProperties",
                column: "PropertyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedProperties_UserID",
                table: "OwnedProperties",
                column: "UserID");

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
                name: "FK_Properties_Users_UserID",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "OwnedProperties");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Properties_UserID",
                table: "Properties");
        }
    }
}

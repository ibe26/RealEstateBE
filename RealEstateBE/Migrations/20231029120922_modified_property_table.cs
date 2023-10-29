using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateBE.Migrations
{
    /// <inheritdoc />
    public partial class modified_property_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Properties",
                newName: "NetArea");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateListed",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GrossArea",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateListed",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "GrossArea",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "NetArea",
                table: "Properties",
                newName: "Size");
        }
    }
}

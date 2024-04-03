using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateDataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class test33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnedProperties_Users_UserID1",
                table: "OwnedProperties");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID1",
                table: "OwnedProperties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedProperties_Users_UserID1",
                table: "OwnedProperties",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnedProperties_Users_UserID1",
                table: "OwnedProperties");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID1",
                table: "OwnedProperties",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedProperties_Users_UserID1",
                table: "OwnedProperties",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtelierAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "InitiativePosts");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "InitiativePosts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "InitiativePosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "DatePublished" },
                values: new object[] { 3, new DateTime(2024, 12, 6, 4, 19, 15, 39, DateTimeKind.Utc).AddTicks(4104) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2024, 12, 6, 4, 19, 15, 38, DateTimeKind.Utc).AddTicks(8875));

            migrationBuilder.CreateIndex(
                name: "IX_InitiativePosts_CategoryId",
                table: "InitiativePosts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InitiativePosts_Category_CategoryId",
                table: "InitiativePosts",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InitiativePosts_Category_CategoryId",
                table: "InitiativePosts");

            migrationBuilder.DropIndex(
                name: "IX_InitiativePosts_CategoryId",
                table: "InitiativePosts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "InitiativePosts");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "InitiativePosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "InitiativePosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Category", "DatePublished" },
                values: new object[] { "Manufacturing & Production", new DateTime(2024, 12, 6, 2, 43, 39, 926, DateTimeKind.Utc).AddTicks(6284) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2024, 12, 6, 2, 43, 39, 926, DateTimeKind.Utc).AddTicks(953));
        }
    }
}

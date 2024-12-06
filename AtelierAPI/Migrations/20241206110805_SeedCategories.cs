using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AtelierAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Category" },
                values: new object[,]
                {
                    { 5, "Sustainability and ESG (Environmental, Social, Governance)" },
                    { 6, "Installation and Deployment" },
                    { 7, "Operational Efficiency" },
                    { 8, "Cost Optimization" },
                    { 9, "Safety and Risk Management" },
                    { 10, "Regulatory Compliance" },
                    { 11, "Customer-Focused Initiatives" },
                    { 12, "-Digitalization and Data Management" }
                });

            migrationBuilder.UpdateData(
                table: "InitiativePosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "DatePublished",
                value: new DateTime(2024, 12, 6, 11, 8, 4, 769, DateTimeKind.Utc).AddTicks(1788));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2024, 12, 6, 11, 8, 4, 768, DateTimeKind.Utc).AddTicks(5366));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "InitiativePosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "DatePublished",
                value: new DateTime(2024, 12, 6, 4, 19, 15, 39, DateTimeKind.Utc).AddTicks(4104));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2024, 12, 6, 4, 19, 15, 38, DateTimeKind.Utc).AddTicks(8875));
        }
    }
}

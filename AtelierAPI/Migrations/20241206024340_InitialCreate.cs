using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AtelierAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Category = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InitiativePosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    DatePublished = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitiativePosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    DateJoined = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Category" },
                values: new object[,]
                {
                    { 1, "Concept Development" },
                    { 2, "Design & Prototyping" },
                    { 3, "Manufacturing & Production" },
                    { 4, "Sustainability & Green Technology" }
                });

            migrationBuilder.InsertData(
                table: "InitiativePosts",
                columns: new[] { "Id", "AuthorId", "Category", "Content", "DatePublished", "Title" },
                values: new object[] { 1, 1, "Manufacturing & Production", "Implement automated robotic systems for precision cutting and assembly of wind turbine blades. This will reduce production time by 20% and minimize material waste.", new DateTime(2024, 12, 6, 2, 43, 39, 926, DateTimeKind.Utc).AddTicks(6284), "Improve Blade Manufacturing Efficiency" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateJoined", "Email", "Password", "Role", "Username" },
                values: new object[] { 1, new DateTime(2024, 12, 6, 2, 43, 39, 926, DateTimeKind.Utc).AddTicks(953), "316333@viauc.dk", "hashed_password", "Admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "InitiativePosts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

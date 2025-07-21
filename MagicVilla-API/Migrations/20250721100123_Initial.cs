using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VillaNumbers",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetAILS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillaID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaNumbers", x => x.VillaNo);
                });

            migrationBuilder.CreateTable(
                name: "Villas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sqft = table.Column<int>(type: "int", nullable: false),
                    Occupancy = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amenity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "Created", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "Updated" },
                values: new object[,]
                {
                    { 1, "Pool, Spa, Gym", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A royal experience with premium amenities and a scenic view.", "https://example.com/images/royalvilla.jpg", "Royal Villa", 4, "200", 550, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Beach, BBQ, Wi-Fi", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Relax in a cozy bungalow right on the beach.", "https://example.com/images/beachside.jpg", "Beachside Bungalow", 2, "300", 450, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Fireplace, Sauna, Trails", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Peaceful retreat in the mountains with hiking trails nearby.", "https://example.com/images/mountain.jpg", "Mountain Retreat", 5, "250", 600, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Wi-Fi, Rooftop Access, Smart TV", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Modern loft in the heart of the city with skyline views.", "https://example.com/images/urbanloft.jpg", "Urban Loft", 2, "180", 350, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Infinity Pool, Hammocks, Desert Tours", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A luxurious escape surrounded by desert beauty.", "https://example.com/images/desertoasis.jpg", "Desert Oasis", 3, "270", 480, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Lake Access, Fireplace, Boat Rental", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Charming lakeside cabin perfect for fishing and relaxing.", "https://example.com/images/lakeviewcabin.jpg", "Lakeview Cabin", 4, "220", 400, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillaNumbers");

            migrationBuilder.DropTable(
                name: "Villas");
        }
    }
}

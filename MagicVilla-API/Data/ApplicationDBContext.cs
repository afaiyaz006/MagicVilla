using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Data;

public class ApplicationDBContext:DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
        
    }
    public DbSet<Villa> Villas { get; set; }
    public DbSet<VillaNumber> VillaNumbers { get; set; }
    public DbSet<LocalUser> LocalUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Villa>().HasData(
            new Villa
            {
                Id = 1,
                Name = "Royal Villa",
                Details = "A royal experience with premium amenities and a scenic view.",
                Rate = "200",
                Sqft = 550,
                Occupancy = 4,
                ImageUrl = "https://example.com/images/royalvilla.jpg",
                Amenity = "Pool, Spa, Gym",
                Created = new DateTime(2023, 01, 01),
                Updated = new DateTime(2023, 01, 01)
            },
            new Villa
            {
                Id = 2,
                Name = "Beachside Bungalow",
                Details = "Relax in a cozy bungalow right on the beach.",
                Rate = "300",
                Sqft = 450,
                Occupancy = 2,
                ImageUrl = "https://example.com/images/beachside.jpg",
                Amenity = "Beach, BBQ, Wi-Fi",
                Created = new DateTime(2023, 01, 01),
                Updated = new DateTime(2023, 01, 01)
            },
            new Villa
            {
                Id = 3,
                Name = "Mountain Retreat",
                Details = "Peaceful retreat in the mountains with hiking trails nearby.",
                Rate = "250",
                Sqft = 600,
                Occupancy = 5,
                ImageUrl = "https://example.com/images/mountain.jpg",
                Amenity = "Fireplace, Sauna, Trails",
                Created = new DateTime(2023, 01, 01),
                Updated = new DateTime(2023, 01, 01)
            },
            new Villa
            {
                Id = 4,
                Name = "Urban Loft",
                Details = "Modern loft in the heart of the city with skyline views.",
                Rate = "180",
                Sqft = 350,
                Occupancy = 2,
                ImageUrl = "https://example.com/images/urbanloft.jpg",
                Amenity = "Wi-Fi, Rooftop Access, Smart TV",
                Created = new DateTime(2023, 01, 01),
                Updated = new DateTime(2023, 01, 01)
            },
            new Villa
            {
                Id = 5,
                Name = "Desert Oasis",
                Details = "A luxurious escape surrounded by desert beauty.",
                Rate = "270",
                Sqft = 480,
                Occupancy = 3,
                ImageUrl = "https://example.com/images/desertoasis.jpg",
                Amenity = "Infinity Pool, Hammocks, Desert Tours",
                Created = new DateTime(2023, 01, 01),
                Updated = new DateTime(2023, 01, 01)
            },
            new Villa
            {
                Id = 6,
                Name = "Lakeview Cabin",
                Details = "Charming lakeside cabin perfect for fishing and relaxing.",
                Rate = "220",
                Sqft = 400,
                Occupancy = 4,
                ImageUrl = "https://example.com/images/lakeviewcabin.jpg",
                Amenity = "Lake Access, Fireplace, Boat Rental",
                Created = new DateTime(2023, 01, 01),
                Updated = new DateTime(2023, 01, 01)
            }
        );
}


}
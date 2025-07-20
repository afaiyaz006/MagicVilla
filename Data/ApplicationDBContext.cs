using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Data;

public class ApplicationDBContext:DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
        
    }
    public DbSet<Villa> Villas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Villa>().HasData(
            new Villa
            {
                Id = 1,
                Name = "Royal Villa",
                Details = "This is a Royal Villa",
                Rate = "2000",
                Sqft = 550,
                Occupancy = 4,
                ImageUrl = "https://example.com/royal-villa.jpg",
                Amenity = "Pool, WiFi, Breakfast",
            },
            new Villa
            {
                Id = 2,
                Name = "Luxury Villa",
                Details = "This is a Luxury Villa",
                Rate = "3000",
                Sqft = 750,
                Occupancy = 6,
                ImageUrl = "https://example.com/luxury-villa.jpg",
                Amenity = "Pool, WiFi, Breakfast, Spa",
             
            }
        );
    }
}
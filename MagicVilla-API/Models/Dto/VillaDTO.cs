using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto;

public class VillaDTO
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Details { get; set; }
    public string Rate { get; set; }
    public int Sqft { get; set; }
    public int Occupancy { get; set; }
    public string ImageUrl { get; set; }
    public string Amenity { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
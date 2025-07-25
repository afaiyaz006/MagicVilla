using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto;

public class VillaCreateDTO
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    public string Details { get; set; }
    public string Rate { get; set; }
    public int Sqft { get; set; }
    public int Occupancy { get; set; }
    public string ImageUrl { get; set; }
    public string Amenity { get; set; }
}
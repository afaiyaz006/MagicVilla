using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto;

public class VillaUpdateDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    [Required]
    public string Details { get; set; }
    [Required]
    public string Rate { get; set; }
    [Required]
    public int Sqft { get; set; }
    [Required]
    public int Occupancy { get; set; }

    public string ImageUrl { get; set; }
    public string Amenity { get; set; }
}
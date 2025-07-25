using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MagicVilla_API.Models.Dto;

public class VillaCreateDTOImage
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    public string Details { get; set; }
    public string Rate { get; set; }
    public int Sqft { get; set; }
    public int Occupancy { get; set; }
    public string ImageUrl { get; set; }
    public IFormFile File { get; set; }
    public string Amenity { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto;

public class VillaNumberUpdateDTO
{
    [Required]
    public int VillaNo { get; set; }
    
    public string SpecialDetails { get; set; }
    [Required]
    public int VillaID { get; set; }
}
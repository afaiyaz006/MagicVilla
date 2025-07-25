using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto;

public class VillaNumberDTO
{
    [Required]
    public int VillaNo { get; set; }
    [Required]
    public int VillaID { get; set; }
    public string SpecialDetails { get; set; }

}
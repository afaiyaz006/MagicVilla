using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Data;

public class VillaStore
{
    public static List<VillaDTO> VillaList = new List<VillaDTO>
    {
        new VillaDTO
        {
            Id = 1,
            Name = "Pool View"
        },
        new VillaDTO
        {
            Id = 2,
            Name = "Lake View"
        },
        new VillaDTO
        {
            Id=3,
            Name="Lake Lake"
        }
    };
}
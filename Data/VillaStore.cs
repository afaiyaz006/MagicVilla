using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Data;

public class VillaStore
{
    public static List<VillaDTO> VillaList = new List<VillaDTO>
    {
        new VillaDTO
        {
            Id = 1,
            Name = "Pool View",
            Sqft = 1,
            Occupancy = 1
        },
        new VillaDTO
        {
            Id = 2,
            Name = "Lake View",
            Sqft = 2,
            Occupancy = 3
        },
        new VillaDTO
        {
            Id=3,
            Name="Lake Lake",
            Sqft = 4,
            Occupancy = 5
        }
    };
}
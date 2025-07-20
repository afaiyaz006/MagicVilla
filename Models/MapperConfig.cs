using AutoMapper;
using MagicVilla_API.Models.Dto;
namespace MagicVilla_API.Models
{
    public class MapperConfig:Profile
    {
        public  MapperConfig()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();
            CreateMap<VillaCreateDTO, Villa>();
            CreateMap<VillaUpdateDTO, Villa>();
            CreateMap<Villa, VillaUpdateDTO>();
            CreateMap<Villa, VillaCreateDTO>();
           
        }
    }
}

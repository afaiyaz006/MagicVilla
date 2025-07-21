
using MagicVilla_Web.Models.Dto;
using AutoMapper;

namespace MagicVilla_Web
{
    public class MapperConfig:Profile
    {
        public  MapperConfig()
        {
            CreateMap<VillaDTO,VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
           
        }
    }
}

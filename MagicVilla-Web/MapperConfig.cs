
using MagicVilla_Web.Models.Dto;
using AutoMapper;
using MagicVilla_API;
using MagicVilla_API.Controllers;

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
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
           
        }
    }
}

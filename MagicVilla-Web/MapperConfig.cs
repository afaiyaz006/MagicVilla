
using MagicVilla_Web.Models.Dto;
using AutoMapper;
using MagicVilla_API;
using MagicVilla_API.Controllers;
using MagicVilla_Web.Models.VM;

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
            CreateMap<VillaNumberCreateVM, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberUpdateVM, VillaNumberUpdateDTO>().ReverseMap();
            

        }
    }
}

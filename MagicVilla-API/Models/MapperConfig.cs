using AutoMapper;
using MagicVilla_API.Controllers;
using MagicVilla_API.Models.Dto;
using Microsoft.Azure.Cosmos;

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
            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();
            CreateMap<LocalUser, UserDTO>().ReverseMap();
           
        }
    }
}

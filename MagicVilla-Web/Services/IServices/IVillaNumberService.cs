using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services;

public interface IVillaNumberService
{
    Task<T> GetAllAsync<T>(string token);
    Task<T> GetAsync<T>(int id,string token);
    Task<T> CreateAsync<T>(VillaNumberCreateDTO dto,string token);
    Task<T> UpdateAsync<T>(int id,VillaNumberUpdateDTO dto,string token);
    Task<T> DeleteAsync<T>(int id,string token);
}
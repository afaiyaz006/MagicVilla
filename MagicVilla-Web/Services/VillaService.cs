using MagicVilla_Utility;
using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services;

public class VillaService:BaseService, IVillaService
{
    private readonly IHttpClientFactory _clientFactory;
    private string villaUrl;
    public VillaService(
        IHttpClientFactory clientFactory,
        IConfiguration configuration
    ) : base(clientFactory)
    {
        _clientFactory = clientFactory;
        villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
    }

    public Task<T> GetAllAsync<T>(string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = villaUrl+"/api/v1/villaAPI",
            Token =  token
        });
    }

    public Task<T> GetAsync<T>(int id,string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = villaUrl+"/api/v1/villaAPI/"+id,
            Token = token
        });
    }

    public Task<T> CreateAsync<T>(VillaCreateDTO dto,string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = dto,
            Url = villaUrl+"/api/v1/villaAPI",
            Token = token
        });
        
    }

    public Task<T> UpdateAsync<T>(int id,VillaUpdateDTO dto,string token)
    {
        return SendAsync<T>(
            new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/v1/villaAPI/"+id,
                Token = token
            });
    }

    public Task<T> DeleteAsync<T>(int id,string token)
    {
        Console.WriteLine("Deleting "+id);
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.DELETE,
            Url = villaUrl+"/api/v1/villaAPI/"+id,
            Token = token
        });
    }
}
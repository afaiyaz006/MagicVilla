using MagicVilla_Utility;
using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services;

public class VillaNumberService:BaseService,IVillaNumberService
{
    private readonly IHttpClientFactory _clientFactory;
    private string villaUrl;
    public VillaNumberService(
        IHttpClientFactory clientFactory,
        IConfiguration configuration
    ) : base(clientFactory)
    {
        _clientFactory = clientFactory;
        villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
    }

    public Task<T> GetAllAsync<T>()
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = villaUrl+"/api/villaNumber"
        });
    }

    public Task<T> GetAsync<T>(int id)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = villaUrl+"/api/villaNumber/"+id
        });
    }

    public Task<T> CreateAsync<T>(VillaNumberCreateDTO dto)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = dto,
            Url = villaUrl+"/api/villaNumber"
        });
        
    }

    public Task<T> UpdateAsync<T>(int id,VillaNumberUpdateDTO dto)
    {
        return SendAsync<T>(
            new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/villaNumber/"+id
            });
    }

    public Task<T> DeleteAsync<T>(int id)
    {
        Console.WriteLine("Deleting "+id);
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.DELETE,
            Url = villaUrl+"/api/villaNumber/"+id
        });
    }
}
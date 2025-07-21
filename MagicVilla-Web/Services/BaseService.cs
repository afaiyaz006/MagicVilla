using MagicVilla_Utility;
using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services;
using MagicVilla_Web.Models;
public class BaseService:IBaseService
{
    public APIResponse responseModel { get; set; }
    public IHttpClientFactory clientFactory { get; set; }
    public BaseService()
    {
        this.responseModel = new();
        
    }

    public Task<T> SendAsync<T>(APIRequest apiRequest)
    {
        throw new NotImplementedException();
    }

}
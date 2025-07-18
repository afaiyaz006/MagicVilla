using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers;
[Route("api/VillAPI")]
[ApiController]
public class VillaApiController : ControllerBase
{
    [HttpGet]
    public IEnumerable<VillaDTO> GetVillas()
    {
        return VillaStore.VillaList;
    }

    [HttpGet("id")]
    public VillaDTO GetVilla(int id)
    {
        return VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
    }
    
    
}
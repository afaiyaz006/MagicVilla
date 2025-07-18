using MagicVilla_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers;
[Route("api/VillaAPI")]
[ApiController]
public class VillaApiController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Villa> GetVillas()
    {
        return new List<Villa>
        {
            new Villa { Id = 1, Name = "Pool View" },
            new Villa { Id = 2, Name = "Lake View" }
        };
    }
}
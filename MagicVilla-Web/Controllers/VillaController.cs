using System.Text.Json;
using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace MagicVilla_Web.Controllers;

public class VillaController:Controller
{
    private readonly IVillaService _villaService;
    private readonly IMapper _mapper;
    
    public VillaController(IVillaService villaService, IMapper mapper)
    {
        _villaService = villaService;
        _mapper = mapper;
    }
 
    
    public async Task<IActionResult> IndexVilla()
    {
        
        List<VillaDTO> list = new();
        // var response = await _villaService.GetAllAsync<APIResponse>();
        // Console.WriteLine(response.StatusCode);
        // Console.WriteLine(response.IsSuccess);
        //
        // if (response != null && response.IsSuccess)
        // {
        //     list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
        //     
        // }
        var response = await _villaService.GetAllAsync<List<VillaDTO>>();
        return View(response);
    }

    // public IActionResult IndexVilla()
    // {
    //     return View();
    // }
}
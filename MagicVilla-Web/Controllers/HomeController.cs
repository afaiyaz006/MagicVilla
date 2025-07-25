using System.Diagnostics;
using MagicVilla_Utility;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IVillaService _villaService;
    public HomeController(IVillaService villaService,ILogger<HomeController> logger)
    {
        _logger = logger;
        _villaService = villaService;
    }

    public async Task<IActionResult> Index()
    {
        List<VillaDTO> list = new();
        var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        // Console.WriteLine(response.StatusCode);
        // Console.WriteLine(response.IsSuccess);
        
        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            
        }
        // var response = await _villaService.GetAllAsync<List<VillaDTO>>();
        // return View(response);
        return View(list);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
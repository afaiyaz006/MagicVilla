using System.Text.Json;
using AutoMapper;
using MagicVilla_Utility;
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
        var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        Console.WriteLine(response.StatusCode);
        Console.WriteLine(response.IsSuccess);
        
        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            
        }
        // var response = await _villaService.GetAllAsync<List<VillaDTO>>();
        // return View(response);
        return View(list);
    }

    public async Task<IActionResult> CreateVilla(VillaCreateDTO model)
    {
        List<VillaDTO> list = new();
        if (ModelState.IsValid)
        {
            var response = await _villaService.CreateAsync<APIResponse>(model,HttpContext.Session.GetString(SD.SessionToken));
            // Console.WriteLine(response.ErrorMessages);
            if (response != null && response.IsSuccess)
            {
                // list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
                return RedirectToAction(nameof(IndexVilla));
            }
    
            return View(model);            
            
        }

        return View(model);

    }

    public async Task<IActionResult> UpdateVilla(int villaId)
    {
        var response = await _villaService.GetAsync<APIResponse>(villaId,HttpContext.Session.GetString(SD.SessionToken));
        if (response != null && response.IsSuccess)
        {
            VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
            return View(_mapper.Map<VillaUpdateDTO>(model));
        }

        return NotFound();


    }
    [HttpPost]
    public async Task<IActionResult> UpdateVilla(VillaUpdateDTO model)
    {
        if (ModelState.IsValid)
        {
            var response = await _villaService.UpdateAsync<APIResponse>(model.Id,model,HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVilla));
            }
        }
        return View(model);
    }

    public async Task<IActionResult> DeleteVilla(int villaId)
    {
        var response = await _villaService.GetAsync<APIResponse>(villaId,HttpContext.Session.GetString(SD.SessionToken));
        if (response != null && response.IsSuccess)
        {
            VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
            return View(_mapper.Map<VillaDTO>(model));
        }

        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> DeleteVilla(VillaDTO model)
    {
        var response = await _villaService.DeleteAsync<APIResponse>(model.Id,HttpContext.Session.GetString(SD.SessionToken));
        Console.WriteLine(response.StatusCode);
        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(IndexVilla));
        }
        Console.WriteLine("Invalid");
        return NotFound();
    }
    
    // public IActionResult IndexVilla()
    // {
    //     return View();
    // }
}
using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.VM;
using MagicVilla_Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers;

public class VillaNumberController:Controller
{
    private readonly IVillaNumberService _villaNumberService;
    private readonly IMapper _mapper;
    
    public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper)
    {
        _villaNumberService = villaNumberService;
        _mapper = mapper;
    }
 
    
    public async Task<IActionResult> IndexVillaNumber()
    {
        
        List<VillaNumberDTO> list = new();
        var response = await _villaNumberService.GetAllAsync<APIResponse>();
        Console.WriteLine(response.StatusCode);
        Console.WriteLine(response.IsSuccess);
        
        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
            
        }
        // var response = await _villaNumberService.GetAllAsync<List<VillaNumberDTO>>();
        // return View(response);
        return View(list);
    }

    public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateDTO model)
    {
        List<VillaNumberDTO> list = new();
        if (ModelState.IsValid)
        {
            var response = await _villaNumberService.CreateAsync<APIResponse>(model);
            
            if (response != null && response.IsSuccess)
            {
                // list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
                return RedirectToAction(nameof(IndexVillaNumber));
            }
    
            return View();            
            
        }

        return View(_mapper.Map<VillaNumberCreateVM>(model));

    }

    public async Task<IActionResult> UpdateVillaNumber(int villaId)
    {
        var response = await _villaNumberService.GetAsync<APIResponse>(villaId);
        if (response != null && response.IsSuccess)
        {
            VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
            return View(_mapper.Map<VillaNumberUpdateVM>(model));
        }

        return NotFound();


    }
    [HttpPost]
    public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateDTO model)
    {
        if (ModelState.IsValid)
        {
            var response = await _villaNumberService.UpdateAsync<APIResponse>(model.VillaNo,model);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }
        }

        return View(_mapper.Map<VillaNumberUpdateVM>(model));
    }

    public async Task<IActionResult> DeleteVillaNumber(int villaId)
    {
        var response = await _villaNumberService.GetAsync<APIResponse>(villaId);
        if (response != null && response.IsSuccess)
        {
            VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
            return View(_mapper.Map<VillaNumberDeleteVM>(model));
        }

        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> DeleteVillaNumber(VillaNumberDTO model)
    {
        var response = await _villaNumberService.DeleteAsync<APIResponse>(model.VillaID);
        Console.WriteLine(response.StatusCode);
        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(IndexVillaNumber));
        }
        Console.WriteLine("Invalid");
        return NotFound();
    }
}
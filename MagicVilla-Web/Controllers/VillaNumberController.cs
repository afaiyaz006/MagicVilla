using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.VM;
using MagicVilla_Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers;

public class VillaNumberController:Controller
{
    private readonly IVillaNumberService _villaNumberService;
    private readonly IMapper _mapper;
    private readonly IVillaService _villaService;
    public VillaNumberController(
        IVillaNumberService villaNumberService,
        IMapper mapper,
        IVillaService villaService
        )
    {
        _villaNumberService = villaNumberService;
        _mapper = mapper;
        _villaService = villaService;
    }
 
    
    public async Task<IActionResult> IndexVillaNumber()
    {
        
        List<VillaNumberDTO> list = new();
        var response = await _villaNumberService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        // Console.WriteLine(response.StatusCode);
        // Console.WriteLine(response.IsSuccess);
        
        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
            
        }
        // var response = await _villaNumberService.GetAllAsync<List<VillaNumberDTO>>();
        // return View(response);
        Console.WriteLine(JsonConvert.SerializeObject(list));
        return View(list);
    }

    public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateDTO model)
    {
        VillaNumberCreateVM villaNumberVM = new();
        var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        if (response != null && response.IsSuccess)
        {
            villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                (Convert.ToString(response.Result)).Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }); ;
        }
        return View(villaNumberVM);
 
    }

    public async Task<IActionResult> UpdateVillaNumber(int villaId)
    {
        var response = await _villaNumberService.GetAsync<APIResponse>(villaId,HttpContext.Session.GetString(SD.SessionToken));
        if (response != null && response.IsSuccess)
        {
            VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
            return View(_mapper.Map<VillaNumberUpdateVM>(model));
        }

        return NotFound();


    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
    {
        if (ModelState.IsValid)
        {

            var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber,
                HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }
            else
            {
                if (response.ErrorMessages.Count > 0)
                {
                    ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                }
            }
        }
        var resp = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        if (resp != null && resp.IsSuccess)
        {
            model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                (Convert.ToString(resp.Result)).Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }); ;
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateDTO model)
    {
        if (ModelState.IsValid)
        {
            var response = await _villaNumberService.UpdateAsync<APIResponse>(model.VillaNo,model,HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }
        }

        return View(_mapper.Map<VillaNumberUpdateVM>(model));
    }

    public async Task<IActionResult> DeleteVillaNumber(int villaId)
    {
        var response = await _villaNumberService.GetAsync<APIResponse>(villaId,HttpContext.Session.GetString(SD.SessionToken));
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
        var response = await _villaNumberService.DeleteAsync<APIResponse>(model.VillaID,HttpContext.Session.GetString(SD.SessionToken));
        Console.WriteLine(response.StatusCode);
        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(IndexVillaNumber));
        }
        Console.WriteLine("Invalid");
        return NotFound();
    }
}
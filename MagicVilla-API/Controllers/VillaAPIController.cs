using System.Data;
using System.Runtime.CompilerServices;
using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers;
[Route("api/VillaAPI")]
[ApiController]
public class VillaApiController : ControllerBase
{
    private readonly ILogger<VillaApiController> _logger;
    //private readonly ApplicationDBContext _db;
    private readonly IVillaRepository _dbVilla;
    private readonly IMapper _mapper;
    public VillaApiController(
        ILogger<VillaApiController> logger,
        IVillaRepository dbVilla,
        IMapper mapper
    )
    {
        _logger = logger;
        _dbVilla  = dbVilla;
        _mapper = mapper;
        
    }
    
    [HttpGet]
    public async  Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
    {
        _logger.LogInformation("Getting All Values");
        IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<VillaDTO>>(villaList));
    }

    [HttpGet("{id:int}",Name="GetVilla")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VillaDTO>> GetVilla(int id)
    {
        if (id <= 0 )
        {
            return BadRequest();
        }
        var villa =await _dbVilla.GetAsync(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(villa);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async  Task<ActionResult<VillaDTO>> CreateVilla([FromBody]VillaCreateDTO villaDTO)
    {
        if (ModelState.IsValid)
        {
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            var villaExists = await _dbVilla.GetAsync(u => u.Name.ToLower() == villaDTO.Name.ToLower());
            if (villaExists!=null)
            {
                ModelState.AddModelError("DuplicateVilla", "Villa name already exists");
                return BadRequest(ModelState);
            }
            Villa villa = _mapper.Map<Villa>(villaDTO);
            //Villa villa = new()
            //{
            //    Name = villaDTO.Name,
            //    Occupancy = villaDTO.Occupancy,
            //    Sqft = villaDTO.Sqft,
            //    ImageUrl = villaDTO.ImageUrl,
            //    Amenity = villaDTO.Amenity,
            //    Details = villaDTO.Details,
            //    Rate = villaDTO.Rate,
            //    Created = DateTime.Now,
            //    Updated = DateTime.Now
            //};
            await _dbVilla.CreateAsync(villa);
            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = await _dbVilla.GetAsync(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        await _dbVilla.RemoveAsync(villa);
        return NoContent();
    }

    [HttpPut("{id:int}",Name="UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateResult(int id,[FromBody]VillaUpdateDTO villaDTO)
    {
        if (villaDTO == null || id != villaDTO.Id)
        {
            return BadRequest();
        }
        //var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
        //Console.WriteLine(villa);
        //villa.Name = villaDTO.Name;
        //villa.Occupancy = villaDTO.Occupancy;
        //villa.Sqft = villaDTO.Sqft;
        //villa.ImageUrl = villaDTO.ImageUrl;
        //Villa villa = new()
        //{
        //    Id = villaDTO.Id,
        //    Name = villaDTO.Name,
        //    Occupancy = villaDTO.Occupancy,
        //    Sqft = villaDTO.Sqft,
        //    ImageUrl = villaDTO.ImageUrl,
        //    Amenity = villaDTO.Amenity,
        //    Details = villaDTO.Details,
        //    Created = DateTime.Now,
        //    Updated = DateTime.Now
        //};
        Villa villa = _mapper.Map<Villa>(villaDTO);
        await _dbVilla.UpdateAsync(villa);
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
    {
        if (patchDTO == null || id == 0)
        {
            return BadRequest();
        }

        var villa = await _dbVilla.GetAsync(u => u.Id == id);

        if (villa == null)
        {
            return BadRequest();
        }

        var villaDTO = _mapper.Map<VillaDTO>(villa);

        patchDTO.ApplyTo(villaDTO, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Map the patched DTO back onto the existing tracked entity
        _mapper.Map(villaDTO, villa);

        // Update the entity (villa is already tracked, so no need to Update())
        villa.Updated = DateTime.Now;
        await _dbVilla.SaveAsync();

        return NoContent();
    }

}
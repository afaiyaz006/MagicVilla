using System.Data;
using System.Runtime.CompilerServices;
using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers;
[Route("api/VillaAPI")]
[ApiController]
public class VillaApiController : ControllerBase
{
    private readonly ILogger<VillaApiController> _logger;
    private readonly ApplicationDBContext _db;
    private readonly IMapper _mapper;
    public VillaApiController(
        ILogger<VillaApiController> logger,
        ApplicationDBContext db,
        IMapper mapper
    )
    {
        _logger = logger;
        _db  = db;
        _mapper = mapper;
        
    }
    
    [HttpGet]
    public async  Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
    {
        _logger.LogInformation("Getting All Values");
        IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
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
        var villa =await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
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
            if (_db.Villas.Any(u => u.Name == villaDTO.Name))
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
            await _db.Villas.AddAsync(villa);
            await _db.SaveChangesAsync();
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
        var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        _db.Villas.Remove(villa);
        await _db.SaveChangesAsync();
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
        _db.Villas.Update(villa);
        await _db.SaveChangesAsync();
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
        
        var villa =await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        
        if (villa == null)
        {
            return BadRequest();
        }
        var villaDTo = _mapper.Map<VillaDTO>(villa);
        //VillaDTO villaDTo = new()
        //{
        //    Amenity = villa.Amenity,
        //    Details = villa.Details,
        //    Id = villa.Id,
        //    ImageUrl = villa.ImageUrl,
        //    Name = villa.Name,
        //    Occupancy = villa.Occupancy,
        //    Rate = villa.Rate,
        //    Sqft = villa.Sqft
        //};
        patchDTO.ApplyTo(villaDTo,ModelState);
        Villa model = _mapper.Map<Villa>(villaDTo);
        //Villa model = new()
        //{
        //    Id = villaDTo.Id,
        //    Name = villaDTo.Name,
        //    Occupancy = villaDTo.Occupancy,
        //    Sqft = villaDTo.Sqft,
        //    ImageUrl = villaDTo.ImageUrl,
        //    Amenity = villaDTo.Amenity,
        //    Details = villaDTo.Details,
        //    Rate = villaDTo.Rate,
        //    Created = DateTime.Now,
        //    Updated = DateTime.Now
        //};
        _db.Villas.Update(model);
        await _db.SaveChangesAsync();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return NoContent();

    }
}
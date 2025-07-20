using System.Data;
using System.Runtime.CompilerServices;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers;
[Route("api/VillaAPI")]
[ApiController]
public class VillaApiController : ControllerBase
{
    private readonly ILogger<VillaApiController> _logger;
    private readonly ApplicationDBContext _db;

    public VillaApiController(
        ILogger<VillaApiController> logger,
        ApplicationDBContext db
        )
    {
        _logger = logger;
        _db  = db;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        _logger.LogInformation("Getting All Values");
        return Ok(_db.Villas);
    }

    [HttpGet("{id:int}",Name="GetVilla")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id <= 0 )
        {
            return BadRequest();
        }
        var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
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
    public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
    {
        if (ModelState.IsValid)
        {
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }

            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (_db.Villas.Any(u => u.Name == villaDTO.Name))
            {
                ModelState.AddModelError("DuplicateVilla", "Villa name already exists");
                return BadRequest(ModelState);
            }
            Villa villa = new()
            {
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Sqft = villaDTO.Sqft,
                ImageUrl = villaDTO.i,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };
            _db.Villas.Add(villa);
            _db.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteVilla(int id)
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
        return NoContent();
    }

    [HttpPut("{id:int}",Name="UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdateResult(int id,[FromBody]VillaDTO villaDTO)
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
        Villa villa = new()
        {
            Id = villaDTO.Id,
            Name = villaDTO.Name,
            Occupancy = villaDTO.Occupancy,
            Sqft = villaDTO.Sqft,
            ImageUrl = villaDTO.ImageUrl,
            Amenity = villaDTO.Amenity,
            Details = villaDTO.Details,
            Created = DateTime.Now,
            Updated = DateTime.Now
        };
        _db.Villas.Update(villa);
        _db.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
    {
        if (patchDTO == null || id == 0)
        {
            return BadRequest();
        }
        
        var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return BadRequest();
        }
        VillaDTO villaDTo = new()
        {
            Amenity = villa.Amenity,
            Details = villa.Details,
            Id = villa.Id,
            ImageUrl = villa.ImageUrl,
            Name = villa.Name,
            Occupancy = villa.Occupancy,
            Rate = villa.Rate,
            Sqft = villa.Sqft
        };
        patchDTO.ApplyTo(villaDTo,ModelState);
        Villa model = new()
        {
            Id = villaDTo.Id,
            Name = villaDTo.Name,
            Occupancy = villaDTo.Occupancy,
            Sqft = villaDTo.Sqft,
            ImageUrl = villaDTo.ImageUrl,
            Amenity = villaDTo.Amenity,
            Details = villaDTo.Details,
            Created = DateTime.Now,
            Updated = DateTime.Now
        };
        _db.Villas.Update(model);
        _db.SaveChanges();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return NoContent();

    }
}
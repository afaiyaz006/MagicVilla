using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/villaNumber")]
    [ApiController]
    public class VillaNumberApiController : ControllerBase
    {
        private readonly ILogger<VillaNumberApiController> _logger;
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IMapper _mapper;

        public VillaNumberApiController(
            ILogger<VillaNumberApiController> logger,
            IVillaNumberRepository dbVillaNumber,
            IMapper mapper
        )
        {
            _logger = logger;
            _dbVillaNumber = dbVillaNumber;
            _mapper = mapper;
        }

        // GET: api/villaNumber
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaNumberDTO>>> GetVillaNumbers()
        {
            var villaNumberList = await _dbVillaNumber.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<VillaNumberDTO>>(villaNumberList));
        }

        // GET: api/villaNumber/{id}
        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        public async Task<ActionResult<VillaNumberDTO>> GetVillaNumber(int id)
        {
            if (id <= 0)
                return BadRequest();

            var villaNumber = await _dbVillaNumber.GetAsync(v => v.VillaNo == id);

            if (villaNumber == null)
                return NotFound();

            return Ok(_mapper.Map<VillaNumberDTO>(villaNumber));
        }

        // POST: api/villaNumber
        [HttpPost]
        public async Task<ActionResult<VillaNumberDTO>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
        {
            if (createDTO == null)
                return BadRequest();

            if (await _dbVillaNumber.GetAsync(v => v.VillaNo == createDTO.VillaNo) != null)
            {
                ModelState.AddModelError("CustomError", "Villa Number already exists!");
                return BadRequest(ModelState);
            }

            var villaNumber = _mapper.Map<VillaNumber>(createDTO);
            villaNumber.CreatedDate = DateTime.Now;
            villaNumber.UpdatedDate = DateTime.Now;

            await _dbVillaNumber.CreateAsync(villaNumber);

            var villaNumberDTO = _mapper.Map<VillaNumberDTO>(villaNumber);

            return CreatedAtRoute("GetVillaNumber", new { id = villaNumberDTO.VillaNo }, villaNumberDTO);
        }

        // PUT: api/villaNumber/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO updateDTO)
        {
            if (updateDTO == null || id <= 0)
                return BadRequest();

            var existingVillaNumber = await _dbVillaNumber.GetAsync(v => v.VillaNo == id, tracked: true);

            if (existingVillaNumber == null)
                return NotFound();

            _mapper.Map(updateDTO, existingVillaNumber);
            existingVillaNumber.UpdatedDate = DateTime.Now;

            await _dbVillaNumber.UpdateAsync(existingVillaNumber);

            return NoContent();
        }

        // DELETE: api/villaNumber/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVillaNumber(int id)
        {
            if (id <= 0)
                return BadRequest();

            var villaNumber = await _dbVillaNumber.GetAsync(v => v.VillaNo == id);

            if (villaNumber == null)
                return NotFound();

            await _dbVillaNumber.RemoveAsync(villaNumber);

            return NoContent();
        }
    }
}

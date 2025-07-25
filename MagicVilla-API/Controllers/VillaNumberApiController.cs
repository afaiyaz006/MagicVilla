using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MagicVilla_Web.Models;

namespace MagicVilla_API.Controllers
{
    [Route("api/v{version:apiVersion}/villaNumber")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VillaNumberApiController : ControllerBase
    {
        private readonly ILogger<VillaNumberApiController> _logger;
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VillaNumberApiController(
            ILogger<VillaNumberApiController> logger,
            IVillaNumberRepository dbVillaNumber,
            IMapper mapper
        )
        {
            _logger = logger;
            _dbVillaNumber = dbVillaNumber;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                var villaNumberList = await _dbVillaNumber.GetAllAsync();
                _response.Result = _mapper.Map<IEnumerable<VillaNumberDTO>>(villaNumberList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode(500, _response);
            }
        }

        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Invalid ID");
                return BadRequest(_response);
            }

            var villaNumber = await _dbVillaNumber.GetAsync(v => v.VillaNo == id);

            if (villaNumber == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
        {
            if (createDTO == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _dbVillaNumber.GetAsync(v => v.VillaNo == createDTO.VillaNo) != null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Villa Number already exists!");
                return BadRequest(_response);
            }

            var villaNumber = _mapper.Map<VillaNumber>(createDTO);
            villaNumber.CreatedDate = DateTime.Now;
            villaNumber.UpdatedDate = DateTime.Now;

            await _dbVillaNumber.CreateAsync(villaNumber);

            var villaNumberDTO = _mapper.Map<VillaNumberDTO>(villaNumber);

            _response.Result = villaNumberDTO;
            _response.StatusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetVillaNumber", new { id = villaNumberDTO.VillaNo }, _response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO updateDTO)
        {
            if (updateDTO == null || id <= 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var existingVillaNumber = await _dbVillaNumber.GetAsync(v => v.VillaNo == id, tracked: true);

            if (existingVillaNumber == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _mapper.Map(updateDTO, existingVillaNumber);
            existingVillaNumber.UpdatedDate = DateTime.Now;

            await _dbVillaNumber.UpdateAsync(existingVillaNumber);

            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var villaNumber = await _dbVillaNumber.GetAsync(v => v.VillaNo == id);

            if (villaNumber == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            await _dbVillaNumber.RemoveAsync(villaNumber);

            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
        
    }
}

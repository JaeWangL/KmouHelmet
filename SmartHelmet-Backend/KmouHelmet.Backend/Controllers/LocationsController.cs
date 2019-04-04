using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using KmouHelmet.Backend.Dtos;
using KmouHelmet.Backend.Mappers;
using KmouHelmet.Backend.Models;
using KmouHelmet.Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KmouHelmet.Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly LocationMapperDtos _mapperDtos;
        private readonly ILocationRepository _locationRepo;

        public LocationsController(LocationMapperDtos mapperDtos, ILocationRepository locationRepo)
        {
            _mapperDtos = mapperDtos;
            _locationRepo = locationRepo;
        }

        [HttpPost]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateLocationAsync([FromBody] AddLocationDto dto)
        {
            if (dto is null)
            {
                return BadRequest();
            }

            LocationModel location = _mapperDtos.MapperToLocationDto(dto);

            await _locationRepo.AddSingleAsync(location);

            return CreatedAtAction(nameof(GetLocationByIdAsync), new { id = location.Id }, location);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetLocationDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<GetLocationDto>>> GetLocationsByAllAsync()
        {
            List<LocationModel> locations = await _locationRepo.GetListByAllAsync();
            if (locations is null)
            {
                return NoContent();
            }

            return Ok(_mapperDtos.MapperToGetDto(locations));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetLocationDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetLocationDto>> GetLocationByIdAsync(int id)
        {
            LocationModel location = await _locationRepo.GetSingleByIdAsync(id);
            if (location is null)
            {
                return NotFound();
            }

            return Ok(_mapperDtos.MapperToGetDto(location));
        }
    }
}

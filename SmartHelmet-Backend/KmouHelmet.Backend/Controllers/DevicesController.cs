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
    public class DevicesController : ControllerBase
    {
        private readonly DeviceMapperDtos _mapperDtos;
        private readonly IDeviceRepository _deviceRepo;

        public DevicesController(DeviceMapperDtos mapperDtos, IDeviceRepository deviceRepo)
        {
            _mapperDtos = mapperDtos;
            _deviceRepo = deviceRepo;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DeviceModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateDeviceAsync([FromBody] AddDeviceDto dto)
        {
            if (dto is null)
            {
                return BadRequest();
            }

            DeviceModel device = _mapperDtos.MapperToDeviceDto(dto);

            await _deviceRepo.AddSingleAsync(device);

            return CreatedAtAction(nameof(GetDeviceByIdAsync), new { id = device.Id }, device);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetDeviceDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetDeviceDto>> GetDeviceByIdAsync(int id)
        {
            DeviceModel device = await _deviceRepo.GetSingleByIdAsync(id);
            if (device is null)
            {
                return NotFound();
            }

            return Ok(_mapperDtos.MapperToGetDto(device));
        }
    }
}

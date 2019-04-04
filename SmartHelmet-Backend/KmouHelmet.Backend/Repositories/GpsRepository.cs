using System.Threading.Tasks;
using KmouHelmet.Backend.Dtos;
using KmouHelmet.Backend.Mappers;
using KmouHelmet.Backend.Models;

namespace KmouHelmet.Backend.Repositories
{
    public class GpsRepository : IGpsRepository
    {
        private readonly GpsMapperDtos _mapperDtos;
        private readonly ILocationRepository _locationRepo;

        public GpsRepository(GpsMapperDtos mapperDtos, ILocationRepository locationRepo)
        {
            _mapperDtos = mapperDtos;
            _locationRepo = locationRepo;
        }

        public async Task AddSingleAsync(GpsDto dto)
        {
            LocationModel isExist = await _locationRepo.GetSingleByDeviceIdAsync(dto.DeviceId);
            if (isExist is null)
            {
                LocationModel location = _mapperDtos.MapperToLocationDto(dto);

                await _locationRepo.AddSingleAsync(location);
            }
            else
            {
                var patchDto = new PatchLocationDto
                {
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude,
                };
                await _locationRepo.PatchSingleAsync(isExist, patchDto);
            }
        }
    }
}

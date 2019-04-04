using System;
using KmouHelmet.Backend.Dtos;
using KmouHelmet.Backend.Models;

namespace KmouHelmet.Backend.Mappers
{
    public class GpsMapperDtos
    {
        public LocationModel MapperToLocationDto(GpsDto dto) =>
            new LocationModel
            {
                DeviceId = dto.DeviceId,
                Latitude = Convert.ToDecimal(dto.Latitude),
                Longitude = Convert.ToDecimal(dto.Longitude),
            };
    }
}

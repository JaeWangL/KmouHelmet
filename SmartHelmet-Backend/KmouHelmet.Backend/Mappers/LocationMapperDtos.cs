using System;
using System.Collections.Generic;
using KmouHelmet.Backend.Dtos;
using KmouHelmet.Backend.Models;

namespace KmouHelmet.Backend.Mappers
{
    public class LocationMapperDtos
    {
        public LocationModel MapperToLocationDto(AddLocationDto dto) =>
            new LocationModel
            {
                DeviceId = dto.DeviceId,
                Latitude = Convert.ToDecimal(dto.Latitude),
                Longitude = Convert.ToDecimal(dto.Longitude),
            };

        public GetLocationDto MapperToGetDto(LocationModel location) =>
            new GetLocationDto
            {
                Id = location.Id,
                DeviceId = location.DeviceId,
                Latitude = Convert.ToDouble(location.Latitude),
                Longitude = Convert.ToDouble(location.Longitude),
            };

        public IEnumerable<GetLocationDto> MapperToGetDto(IEnumerable<LocationModel> locations)
        {
            var results = new List<GetLocationDto>();

            foreach (LocationModel location in locations)
            {
                results.Add(MapperToGetDto(location));
            }

            return results;
        }
    }
}

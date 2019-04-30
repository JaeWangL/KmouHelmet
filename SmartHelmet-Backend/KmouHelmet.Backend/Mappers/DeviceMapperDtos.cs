using System.Collections.Generic;
using KmouHelmet.Backend.Dtos;
using KmouHelmet.Backend.Models;

namespace KmouHelmet.Backend.Mappers
{
    public class DeviceMapperDtos
    {
        public DeviceModel MapperToDeviceDto(AddDeviceDto dto) =>
            new DeviceModel
            {
                StreamingUrl = dto.StreamingUrl,
            };

        public GetDeviceDto MapperToGetDto(DeviceModel device) =>
            new GetDeviceDto
            {
                Id = device.Id,
                StreamingUrl = device.StreamingUrl,
            };

        public IEnumerable<GetDeviceDto> MapperToGetDto(IEnumerable<DeviceModel> devices)
        {
            var results = new List<GetDeviceDto>();

            foreach (DeviceModel device in devices)
            {
                results.Add(MapperToGetDto(device));
            }

            return results;
        }
    }
}

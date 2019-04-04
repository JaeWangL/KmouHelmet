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
    }
}

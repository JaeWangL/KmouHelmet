using System.Collections.Generic;
using System.Threading.Tasks;
using KmouHelmet.Mobile.Dtos;

namespace KmouHelmet.Mobile.Services.Device
{
    public interface IDeviceService
    {
        Task<IEnumerable<DeviceDto>> GetAllDevicesAsync();
    }
}

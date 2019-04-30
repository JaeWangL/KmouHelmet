using System.Collections.Generic;
using System.Threading.Tasks;
using KmouHelmet.Backend.Models;

namespace KmouHelmet.Backend.Repositories
{
    public interface IDeviceRepository
    {
        Task<DeviceModel> AddSingleAsync(DeviceModel device);

        Task<DeviceModel> GetSingleByIdAsync(int id);

        Task<List<DeviceModel>> GetListByAllAsync();
    }
}

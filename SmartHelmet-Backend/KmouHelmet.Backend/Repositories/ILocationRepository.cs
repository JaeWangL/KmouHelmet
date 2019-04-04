using System.Collections.Generic;
using System.Threading.Tasks;
using KmouHelmet.Backend.Dtos;
using KmouHelmet.Backend.Models;

namespace KmouHelmet.Backend.Repositories
{
    public interface ILocationRepository
    {
        Task<LocationModel> AddSingleAsync(LocationModel location);

        Task<LocationModel> GetSingleByIdAsync(int id);

        Task<LocationModel> GetSingleByDeviceIdAsync(int deviceId);

        Task<List<LocationModel>> GetListByAllAsync();

        Task<LocationModel> PatchSingleAsync(LocationModel location, PatchLocationDto dto);
    }
}

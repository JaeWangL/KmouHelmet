using System.Collections.Generic;
using System.Threading.Tasks;
using KmouHelmet.Mobile.Dtos;

namespace KmouHelmet.Mobile.Services.Location
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
    }
}

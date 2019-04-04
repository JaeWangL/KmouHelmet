using System.Threading.Tasks;
using KmouHelmet.Backend.Dtos;

namespace KmouHelmet.Backend.Repositories
{
    public interface IGpsRepository
    {
        Task AddSingleAsync(GpsDto dto);
    }
}

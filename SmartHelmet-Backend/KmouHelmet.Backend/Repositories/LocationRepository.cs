using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KmouHelmet.Backend.Dtos;
using KmouHelmet.Backend.Infrastructure;
using KmouHelmet.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace KmouHelmet.Backend.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly KmouDbContext _context;

        public LocationRepository(KmouDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<LocationModel> AddSingleAsync(LocationModel location)
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();

            return location;
        }

        public async Task<LocationModel> GetSingleByIdAsync(int id)
        {
            LocationModel device = await _context.Locations
                .SingleOrDefaultAsync(d => d.Id == id);

            return device;
        }

        public async Task<LocationModel> GetSingleByDeviceIdAsync(int deviceId)
        {
            LocationModel device = await _context.Locations
                .SingleOrDefaultAsync(l => l.DeviceId == deviceId);

            return device;
        }

        public async Task<List<LocationModel>> GetListByAllAsync()
        {
            List<LocationModel> devices = await _context.Locations.ToListAsync();

            return devices;
        }

        public async Task<LocationModel> PatchSingleAsync(LocationModel location, PatchLocationDto dto)
        {
            if (dto.Latitude.HasValue)
            {
                location.Latitude = Convert.ToDecimal(dto.Latitude.Value);
            }
            if (dto.Longitude.HasValue)
            {
                location.Longitude = Convert.ToDecimal(dto.Longitude.Value);
            }

            await _context.SaveChangesAsync();

            return location;
        }
    }
}

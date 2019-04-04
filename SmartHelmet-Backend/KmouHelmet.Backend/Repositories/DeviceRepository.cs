using System;
using System.Threading.Tasks;
using KmouHelmet.Backend.Infrastructure;
using KmouHelmet.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace KmouHelmet.Backend.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly KmouDbContext _context;

        public DeviceRepository(KmouDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<DeviceModel> AddSingleAsync(DeviceModel device)
        {
            await _context.Devices.AddAsync(device);
            await _context.SaveChangesAsync();

            return device;
        }

        public async Task<DeviceModel> GetSingleByIdAsync(int id)
        {
            DeviceModel device = await _context.Devices
                .SingleOrDefaultAsync(d => d.Id == id);

            return device;
        }
    }
}

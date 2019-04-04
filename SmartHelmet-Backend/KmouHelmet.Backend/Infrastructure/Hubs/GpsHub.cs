using System;
using System.Threading.Tasks;
using KmouHelmet.Backend.Dtos;
using KmouHelmet.Backend.Repositories;
using KmouHelmet.Backend.Utils;
using Microsoft.AspNetCore.SignalR;

namespace KmouHelmet.Backend.Infrastructure.Hubs
{
    public class GpsHub : Hub
    {
        private readonly IGpsRepository _gpsRepo;

        public GpsHub(IGpsRepository gpsRepo)
        {
            _gpsRepo = gpsRepo;
        }

        public async Task SendDataAsync(string deviceId, string gpsData)
        {
            if (!string.IsNullOrEmpty(deviceId) && !string.IsNullOrEmpty(gpsData))
            {
                var data = NmeaUtils.Parse(gpsData);
                var gpsDto = new GpsDto
                {
                    DeviceId = Convert.ToInt32(deviceId),
                    Latitude = NmeaUtils.StringToLatitude(data[2], data[3]),
                    Longitude = NmeaUtils.StringToLongitude(data[4], data[5]),
                };

                await _gpsRepo.AddSingleAsync(gpsDto);

                await Clients.All.SendAsync("ReceivedData", gpsDto);
            }
        }
    }
}

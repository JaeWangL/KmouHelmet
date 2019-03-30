using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SmartHelmet.SignalrHub.Dtos;
using SmartHelmet.SignalrHub.Helpers;

namespace SmartHelmet.SignalrHub.Hubs
{
    public class GpsHub : Hub
    {
        public async Task SendData(string deviceId, string gpsData)
        {
            if (!string.IsNullOrEmpty(deviceId) && !string.IsNullOrEmpty(gpsData))
            {
                var data = NmeaHelpers.Parse(gpsData);
                var result = new GpsDto
                {
                    DeviceId = Convert.ToInt32(deviceId),
                    Latitude = NmeaHelpers.StringToLatitude(data[2], data[3]),
                    Longitude = NmeaHelpers.StringToLongitude(data[4], data[5]),
                };

                await Clients.All.SendCoreAsync("ReceivedData",
                    new object[] { deviceId, NmeaHelpers.StringToLatitude(data[2], data[3]), NmeaHelpers.StringToLongitude(data[4], data[5]) } );
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KmouHelmet.Mobile.Dtos;
using KmouHelmet.Mobile.Extensions;
using KmouHelmet.Mobile.Services.Request;
using Xamarin.Forms;

namespace KmouHelmet.Mobile.Services.Device
{
    public class DeviceService : IDeviceService
    {
        readonly IRequestService _requestService;

        public DeviceService()
        {
            _requestService = DependencyService.Get<IRequestService>();
        }

        public Task<IEnumerable<DeviceDto>> GetAllDevicesAsync()
        {
            var builder = new UriBuilder(AppSettings.BackendEndPoint);
            builder.AppendToPath("api/v1/Devices");

            var uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<DeviceDto>>(uri);
        }
    }
}

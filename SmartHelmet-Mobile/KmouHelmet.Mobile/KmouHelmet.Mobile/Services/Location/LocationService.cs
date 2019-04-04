using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KmouHelmet.Mobile.Dtos;
using KmouHelmet.Mobile.Extensions;
using KmouHelmet.Mobile.Services.Request;
using Xamarin.Forms;

namespace KmouHelmet.Mobile.Services.Location
{
    public class LocationService : ILocationService
    {
        readonly IRequestService _requestService;

        public LocationService()
        {
            _requestService = DependencyService.Get<IRequestService>();
        }

        public Task<IEnumerable<LocationDto>> GetAllLocationsAsync()
        {
            var builder = new UriBuilder(AppSettings.BackendEndPoint);
            builder.AppendToPath("api/v1/Locations");

            var uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<LocationDto>>(uri);
        }
    }
}

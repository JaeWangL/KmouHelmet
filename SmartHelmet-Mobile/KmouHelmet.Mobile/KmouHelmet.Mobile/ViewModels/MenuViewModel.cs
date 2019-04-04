using System.Collections.Generic;
using System.Threading.Tasks;
using KmouHelmet.Mobile.Dtos;
using KmouHelmet.Mobile.Services.Location;
using MvvmHelpers;
using OperationResult;
using Xamarin.Forms;

namespace KmouHelmet.Mobile.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        readonly ILocationService _locationService;

        public MenuViewModel()
        {
            _locationService = DependencyService.Get<ILocationService>();
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            Result<IEnumerable<LocationDto>> locationsResult =
                await TryExecuteWithLoadingIndicatorsAsync(_locationService.GetAllLocationsAsync());

            if (locationsResult)
            {
                Locations.ReplaceRange(locationsResult.Value);
            }
        }

        public ObservableRangeCollection<LocationDto> Locations { get; } =
            new ObservableRangeCollection<LocationDto>();
    }
}

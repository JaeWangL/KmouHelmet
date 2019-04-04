using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using KmouHelmet.Mobile.Dtos;
using KmouHelmet.Mobile.Services.Location;
using KmouHelmet.Mobile.Utils;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmHelpers;
using OperationResult;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using XF.Material.Forms.UI.Dialogs;

namespace KmouHelmet.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        readonly HubConnection _hubConnection;
        readonly ILocationService _locationService;
        bool _isConnected;
        ObservableRangeCollection<LocationDto> _locations;

        public ICommand SendCommand => new AsyncCommand(SendAsync);

        public HomeViewModel()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{AppSettings.BackendEndPoint}gpshub")
                .Build();
            _locationService = DependencyService.Get<ILocationService>();
            _locations = new ObservableRangeCollection<LocationDto>();

            _hubConnection.Closed += async (error) =>
            {
                _isConnected = false;
                await MaterialDialog.Instance.AlertAsync($"Hub Connection is closed: {error}");
                await Task.Delay(1000);
                await ConnectAsync();
            };

            _hubConnection.On<GpsDto>("ReceivedData", (data) =>
            {
                var pin = new Pin()
                {
                    Type = PinType.Generic,
                    Label = data.DeviceId.ToString(),
                    Position = new Position(data.Latitude, data.Longitude),
                };
                Pins.Add(pin);
            });
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            Result<IEnumerable<LocationDto>> locationsResult =
                await TryExecuteWithLoadingIndicatorsAsync(_locationService.GetAllLocationsAsync());

            if (locationsResult)
            {
                _locations.ReplaceRange(locationsResult.Value);
                foreach (LocationDto location in _locations)
                {
                    var pin = new Pin()
                    {
                        Type = PinType.Generic,
                        Label = location.DeviceId.ToString(),
                        Position = new Position(location.Latitude, location.Longitude),
                    };
                    Pins.Add(pin);
                }
            }
        }

        public ObservableRangeCollection<Pin> Pins { get; } =
            new ObservableRangeCollection<Pin>();

        public async Task ConnectAsync()
        {
            if (_isConnected)
            {
                return;
            }

            await _hubConnection.StartAsync();
            _isConnected = true;
        }

        public async Task SendAsync()
        {
            try
            {
                await ConnectAsync();
                await _hubConnection.SendAsync("SendDataAsync", "8", "GPRMC,161006.425,A,7855.6020,S,13843.8900,E,154.89,84.62,110715,173.1,W,A*30");

                System.Diagnostics.Debug.WriteLine("정상");
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("에러: " + ex);
            }
        }
    }
}

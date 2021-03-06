﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using KmouHelmet.Mobile.Dtos;
using KmouHelmet.Mobile.Services.Device;
using KmouHelmet.Mobile.Services.Location;
using KmouHelmet.Mobile.Utils;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmHelpers;
using OperationResult;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using XF.Material.Forms.UI.Dialogs;

namespace KmouHelmet.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        readonly HubConnection _hubConnection;
        readonly IDeviceService _deviceService;
        readonly ILocationService _locationService;
        Pin _selPin;
        string _selDeviceId = string.Empty, _selPosition;
        bool _isConnected;
        ObservableRangeCollection<LocationDto> _locations;
        ObservableRangeCollection<Pin> _pins;

        public ICommand RefreshDataCommand => new AsyncCommand(RefreshDataAsync);
        public ICommand ViewCameraCommand => new AsyncCommand(ViewCameraAsync);

        public HomeViewModel()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{AppSettings.BackendEndPoint}gpshub")
                .Build();
            _deviceService = DependencyService.Get<IDeviceService>();
            _locationService = DependencyService.Get<ILocationService>();
            _locations = new ObservableRangeCollection<LocationDto>();
            _pins = new ObservableRangeCollection<Pin>();

            _hubConnection.Closed += async (error) =>
            {
                _isConnected = false;
                await MaterialDialog.Instance.AlertAsync($"Hub Connection is closed: {error}");
                await Task.Delay(1000);
                await ConnectAsync();
            };

            _hubConnection.On<GpsDto>("ReceivedData", (data) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    // AddPin(9, 35.075496, 129.087850); for test
                    AddPin(data.DeviceId, data.Latitude, data.Longitude);
                });
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
                    AddPin(location.DeviceId, location.Latitude, location.Longitude);
                }
            }
        }

        public Pin SelPin
        {
            get => _selPin;
            set
            {
                SetAndRaisePropertyChanged(ref _selPin, value);
                UpdateSelectedPin();
            }
        }

        public string SelDeviceId
        {
            get => _selDeviceId;
            set => SetAndRaisePropertyChanged(ref _selDeviceId, value);
        }

        public string SelPosition
        {
            get => _selPosition;
            set => SetAndRaisePropertyChanged(ref _selPosition, value);
        }

        public ObservableRangeCollection<Pin> Pins
        {
            get => _pins;
            set => SetAndRaisePropertyChanged(ref _pins, value);
        }

        public async Task ConnectAsync()
        {
            if (_isConnected)
            {
                return;
            }

            await _hubConnection.StartAsync();
            _isConnected = true;
        }

        public async Task RefreshDataAsync()
        {
            Pins.Clear();
            Result<IEnumerable<LocationDto>> locationsResult =
                await TryExecuteWithLoadingIndicatorsAsync(_locationService.GetAllLocationsAsync());

            if (locationsResult)
            {
                _locations.ReplaceRange(locationsResult.Value);
                foreach (LocationDto location in _locations)
                {
                    AddPin(location.DeviceId, location.Latitude, location.Longitude);
                }
            }
        }

        public async Task ViewCameraAsync()
        {
            try
            {
                IsBusy = true;

                DeviceDto test = await _deviceService.GetDeviceByIdAsync(Convert.ToInt32(_selPin.Label));
                await Browser.OpenAsync(test.StreamingUrl, BrowserLaunchMode.SystemPreferred);
               
                // await ConnectAsync();
                // await _hubConnection.SendAsync("SendDataAsync", "8", "GPRMC,161006.425,A,7855.6020,S,13843.8900,E,154.89,84.62,110715,173.1,W,A*30");

                IsBusy = false;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("HomeViewModel.SendAsync: " + ex);
            }
        }

        void AddPin(int deviceId, double latitude, double longitude)
        {
            try
            {
                var pin = new Pin()
                {
                    Icon = deviceId == 9 ?
                    BitmapDescriptorFactory.DefaultMarker(Color.Blue) : BitmapDescriptorFactory.DefaultMarker(Color.Red),
                    Type = PinType.Place,
                    Label = deviceId.ToString(),
                    Position = new Position(latitude, longitude)
                };
                Pins.Add(pin);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("HomeViewModel.AddPin: " + ex);
            }
        }

        void UpdateSelectedPin()
        {
            SelDeviceId = "DeviceId: " + _selPin.Label;
            SelPosition = 
                $"Position: {_selPin.Position.Latitude.ToString()}, {_selPin.Position.Longitude.ToString()}";
        }
    }
}

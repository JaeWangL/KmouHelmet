using System.Collections.Generic;
using System.Threading.Tasks;
using KmouHelmet.Mobile.Dtos;
using KmouHelmet.Mobile.Services.Device;
using MvvmHelpers;
using OperationResult;
using Xamarin.Forms;

namespace KmouHelmet.Mobile.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        readonly IDeviceService _deviceService;

        public MenuViewModel()
        {
            _deviceService = DependencyService.Get<IDeviceService>();
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            Result<IEnumerable<DeviceDto>> devicesResult =
                await TryExecuteWithLoadingIndicatorsAsync(_deviceService.GetAllDevicesAsync());

            if (devicesResult)
            {
                Devices.ReplaceRange(devicesResult.Value);
            }
        }

        public ObservableRangeCollection<DeviceDto> Devices { get; } =
            new ObservableRangeCollection<DeviceDto>();
    }
}

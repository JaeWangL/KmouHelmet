using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using KmouHelmet.Mobile.Helpers;
using OperationResult;

namespace KmouHelmet.Mobile.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        bool _isBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsBusy
        {
            get => _isBusy;
            set => SetAndRaisePropertyChanged(ref _isBusy, value);
        }

        public virtual Task InitializeAsync() => Task.CompletedTask;

        public virtual Task UninitializeAsync() => Task.CompletedTask;

        protected async Task<Status> TryExecuteWithLoadingIndicatorsAsync(
            Task operation,
            Func<Exception, Task<bool>> onError = null) =>
            await TaskHelpers.Create()
                .WhenStarting(() => IsBusy = true)
                .WhenFinished(() => IsBusy = false)
                .TryWithErrorHandlingAsync(operation, onError);

        protected async Task<Result<T>> TryExecuteWithLoadingIndicatorsAsync<T>(
            Task<T> operation,
            Func<Exception, Task<bool>> onError = null) =>
            await TaskHelpers.Create()
                .WhenStarting(() => IsBusy = true)
                .WhenFinished(() => IsBusy = false)
                .TryWithErrorHandlingAsync(operation, onError);

        protected void SetAndRaisePropertyChanged<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetAndRaisePropertyChangedIfDifferentValues<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
            where TRef : class
        {
            if (field == null || !field.Equals(value))
            {
                SetAndRaisePropertyChanged(ref field, value, propertyName);
            }
        }
    }
}

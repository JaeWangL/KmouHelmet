using System;
using System.Net.Http;
using System.Threading.Tasks;
using OperationResult;
using Xamarin.Essentials;
using XF.Material.Forms.UI.Dialogs;
using static OperationResult.Helpers;

namespace KmouHelmet.Mobile.Helpers
{
    public class TaskHelpers
    {
        Action _whenStarting;
        Action _whenFinished;

        public static TaskHelpers Create() => new TaskHelpers();

        public TaskHelpers WhenStarting(Action action)
        {
            _whenStarting = action;

            return this;
        }

        public TaskHelpers WhenFinished(Action action)
        {
            _whenFinished = action;

            return this;
        }

        public async Task<Status> TryWithErrorHandlingAsync(
            Task task,
            Func<Exception, Task<bool>> customErrorHandler = null)
        {
            var taskWrapper = new Func<Task<object>>(() => WrapTaskAsync(task));
            Result<object> result = await TryWithErrorHandlingAsync(taskWrapper(), customErrorHandler);

            if (result)
            {
                return Ok();
            }

            return Error();
        }

        public async Task<Result<T>> TryWithErrorHandlingAsync<T>(
            Task<T> task,
            Func<Exception, Task<bool>> customErrorHandler = null)
        {
            _whenStarting?.Invoke();

            if (!(Connectivity.NetworkAccess == NetworkAccess.Internet))
            {
                await MaterialDialog.Instance.AlertAsync("There's no Internet access.");
                return Error();
            }

            try
            {
                T actualResult = await task;
                return Ok(actualResult);
            }
            catch (HttpRequestException exception)
            {
                if (customErrorHandler == null || !await customErrorHandler?.Invoke(exception))
                {
                    await MaterialDialog.Instance.AlertAsync("InternetError. Please try again later.");
                }
            }
            catch (Exception exception)
            {
                if (customErrorHandler == null || !await customErrorHandler?.Invoke(exception))
                {
                    await MaterialDialog.Instance.AlertAsync("InternetError. Please try again later.");
                }
            }
            finally
            {
                _whenFinished?.Invoke();
            }

            return Error();
        }

        private async Task<object> WrapTaskAsync(Task innerTask)
        {
            await innerTask;

            return new object();
        }
    }
}

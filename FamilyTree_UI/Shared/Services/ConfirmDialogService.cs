using FamilyTree_UI.Shared.Models;

namespace FamilyTree_UI.Shared.Services
{
    public class ConfirmDialogService
    {
        private TaskCompletionSource<bool>? _taskCompletionSource;

        public event Action<string, string, ConfirmDialogOptions>? OnShow;

        public Task<bool> ShowAsync(string title, string message1, ConfirmDialogOptions options)
        {
            _taskCompletionSource = new TaskCompletionSource<bool>();
            OnShow?.Invoke(title, message1, options);
            return _taskCompletionSource.Task;
        }

        public void Confirm(bool confirmed)
        {
            _taskCompletionSource?.SetResult(confirmed);
        }
    }
}

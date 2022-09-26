﻿
namespace Functionland.FxFiles.Client.Shared.Components.Modal
{
    public partial class ConfirmationModal
    {
        private string? _title { get; set; }
        private string? _description { get; set; }
        private bool _isModalOpen { get; set; }

        private TaskCompletionSource<ConfirmationModalResult>? _tcs = new();

        public async Task<ConfirmationModalResult> ShowAsync(string title, string description)
        {
            _title = title;
            _description = description;

            _tcs?.SetCanceled();

            _isModalOpen = true;
            StateHasChanged();

            _tcs = new TaskCompletionSource<ConfirmationModalResult>();

            return await _tcs.Task;
        }

        private void Confirm()
        {
            var result = new ConfirmationModalResult();

            result.ResultType = ConfirmationModalResultType.Confirm;

            _tcs!.SetResult(result);
            _tcs = null;
            _isModalOpen = false;
        }

        private void Close()
        {
            var result = new ConfirmationModalResult();

            result.ResultType = ConfirmationModalResultType.Cancel;

            _tcs!.SetResult(result);
            _tcs = null;
            _isModalOpen = false;
        }

        public void Dispose()
        {
            _tcs?.SetCanceled();
        }
    }
}
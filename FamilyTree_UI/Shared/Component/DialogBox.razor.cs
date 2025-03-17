using FamilyTree_UI.Shared.Models;
using FamilyTree_UI.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace FamilyTree_UI.Shared.Component
{
    public partial class DialogBox
    {
        private bool isVisible;
        private string Title { get; set; }
        private string Message1 { get; set; }
        private ConfirmDialogOptions Options { get; set; }

        [Inject]
        private ConfirmDialogService ConfirmDialogService { get; set; }

        protected override void OnInitialized()
        {
            ConfirmDialogService.OnShow += ShowDialog;
        }

        private void ShowDialog(string title, string message, ConfirmDialogOptions options)
        {
            Title = title;
            Message1 = message;
            Options = options;
            isVisible = true;
            StateHasChanged();
        }

        private void Confirm()
        {
            ConfirmDialogService.Confirm(true);
            isVisible = false;
        }

        private void Cancel()
        {
            ConfirmDialogService.Confirm(false);
            isVisible = false;
        }

        private string GetYesButtonColor() => $"btn-{Options.YesButtonColor.ToString().ToLower()}";
        private string GetNoButtonColor() => $"btn-{Options.NoButtonColor.ToString().ToLower()}";
        private string GetModalSize() => Options.Size switch
        {
            DialogSize.Small => "modal-sm",
            DialogSize.Large => "modal-lg",
            DialogSize.ExtraLarge => "modal-xl",
            _ => ""
        };
    }
}
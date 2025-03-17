using Microsoft.AspNetCore.Components;

namespace FamilyTree_UI.Shared.Component
{
    public partial class Loader
    {
        private bool isVisible = false;

        public void ShowLoader()
        {
            InvokeAsync(() =>
            {
                isVisible = true;
                StateHasChanged();  
            });
        }

        public void HideLoader()
        {
            InvokeAsync(() =>
            {
                isVisible = false;
                StateHasChanged();
            });
        }

    }
}
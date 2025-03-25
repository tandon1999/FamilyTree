using Microsoft.JSInterop;

namespace FamilyTree_UI.Shared
{
    public partial class Footer
    {
        private async Task ScrollToTop()
        {
            await JS.InvokeVoidAsync("scrollToTop");
        }
    }
}
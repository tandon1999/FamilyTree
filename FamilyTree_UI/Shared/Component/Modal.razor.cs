using Microsoft.AspNetCore.Components;

namespace FamilyTree_UI.Shared.Component
{
    public partial class Modal
    {
        [Parameter] public string Title { get; set; } = "Modal Title";
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public bool Show { get; set; }
        [Parameter] public string Size { get; set; } = "lg"; 
        [Parameter] public bool ShowSaveButton { get; set; } = true; 
        [Parameter] public string ButtonType { get; set; } = "Save"; 

        private string ModalDisplay => Show ? "block" : "none";
        private string ModalClass => Show ? "show" : "";
        private string ModalSizeClass => Size switch
        {
            "sm" => "modal-sm",
            "lg" => "modal-lg",
            "xl" => "modal-xl",
            _ => ""
        };
        private bool IsVisible => Show;

        private void CloseModal()
        {
            OnClose.InvokeAsync();
        }

        private void SaveChanges()
        {
            if (ShowSaveButton)
            {
                OnSave.InvokeAsync(); 
            }
        }
    }
}
using FamilyTree_UI.Shared.Managers.Interface;
using FamilyTree_UI.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FamilyTree_UI.Shared.Component
{
    public partial class AutoCompleteSelect : ComponentBase
    {
        [Parameter]
        public string Type { get; set; } = string.Empty;
        [Parameter]
        public string Filter { get; set; } = string.Empty;
        [Parameter]
        public int TabIndex { get; set; }
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public string Value { get; set; } = string.Empty;
        [Parameter]
        public int All { get; set; } = 0;
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }
        [Parameter]
        public bool AllowClear { get; set; } = false;
        [Parameter]
        public string Placeholder { get; set; } = string.Empty;
        [Parameter]
        public List<DropDownListModel> DropDownModels { get; set; } = new();
        [Inject] private IAuthUtilityManager AuthUtilityManager { get; set; } = default!;
        private string SelectedText { get; set; } = string.Empty;
        private string _value = string.Empty;
        public string BindingValue
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                ValueChanged.InvokeAsync(value);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (DropDownModels.Count == 0)
                    await LoadDropDown();
                BindingValue = Value;
            }
            catch (Exception ex)
            {
                //await JSRuntime.InvokeVoidAsync("toastr.error", ex.Message);
            }
        }
        protected override async Task OnParametersSetAsync()
        {
            //await LoadDropDown();

            if (!string.IsNullOrEmpty(Value))
            {
                BindingValue = Value;
            }
        }
        private async Task LoadDropDown()
        {
           DropDownModels = await AuthUtilityManager.GetDropDownListAsync(DropDownType: Type, AllorSelect: All, Filter1: Filter);
        }
    }

}
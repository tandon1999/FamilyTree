using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FamilyTree_UI.Shared.Component
{
    public partial class Date
    {
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }
        [Parameter]
        public string InitialValue { get; set; } = string.Empty;
        [Parameter]
        public int TabIndex { get; set; }
        [Parameter]
        public bool Disabled { get; set; } = false;
        private string Id { get; set; } = Guid.NewGuid().ToString();
        [Inject] IJSRuntime _JSRuntime { get; set; }

        private string _value = "";
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


        public void ChangedDate(string date)
        {
            BindingValue = date;
        }
        protected override async Task OnAfterRenderAsync(bool firstrender)
        {
            var datetype = "NP";
            if (firstrender)
            {
                await _JSRuntime.InvokeVoidAsync("LoadDatePicker", Id, datetype);
            }
            if (!string.IsNullOrEmpty(InitialValue))
            {
                BindingValue = InitialValue;
            }
        }
    }

}
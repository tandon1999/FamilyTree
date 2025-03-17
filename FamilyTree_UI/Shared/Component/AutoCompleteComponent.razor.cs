using FamilyTree_UI.Shared.Managers.Interface;
using FamilyTree_UI.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FamilyTree_UI.Shared.Component
{
    public partial class AutoCompleteComponent : ComponentBase
    {

        [Parameter]
        public string SearchText { get; set; } = string.Empty;
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
        public List<DropDownListModel> FilteredItems { get; set; } = new();
        [Parameter] public EventCallback<int> ItemSelected { get; set; }
        private bool IsSearching { get; set; } = false;
        bool ShowItemList = false;
        private string SelectedText { get; set; } = string.Empty;
        private string _value = string.Empty;
        [Inject] private IAuthUtilityManager AuthUtilityManager { get; set; } = default!;
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

            }
        }

        public async Task OnItemClick(DropDownListModel item)
        {

            SearchText = item.Value;
            Value = item.Id;
            BindingValue = item.Id;
            IsSearching = false;
        }
        public async Task bindText(ChangeEventArgs e)
        {
            var text = e.Value.ToString();
            SearchText = text;
            IsSearching = true;
            FilteredItems = DropDownModels.Where(x => x.Value.ToLower().Contains(SearchText.ToLower()))
                .ToList();
        }

        public async Task ShowList()
        {
            await Task.Delay(200);
            ShowItemList = true;
        }

        public async Task HideList()
        {
            await Task.Delay(200);
            ShowItemList = false;
            StateHasChanged();

        }
        private async Task LoadDropDown()
        {
            DropDownModels = await AuthUtilityManager.GetDropDownListAsync(DropDownType: Type, AllorSelect: All, Filter1: Filter);
            FilteredItems = DropDownModels;
        }
    }
}
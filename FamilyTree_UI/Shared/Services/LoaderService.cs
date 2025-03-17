namespace FamilyTree_UI.Shared.Services
{
    public class LoaderService
    {
        private Action? showAction;
        private Action? hideAction;
        public void RegisterShowAction(Action showAction) => this.showAction = showAction;
        public void RegisterHideAction(Action hideAction) => this.hideAction = hideAction;

        public void ShowLoader() => showAction?.Invoke();
        public void HideLoader() => hideAction?.Invoke();
    }
}

namespace FamilyTree_UI.Shared.Services
{
    public class NavStateService
    {
        public bool IsNavVisible { get; private set; } = false;

        public event Action OnNavStateChanged;

        public void SetNavVisibility(bool isVisible)
        {
            IsNavVisible = isVisible;
            OnNavStateChanged?.Invoke();
        }
    }
}

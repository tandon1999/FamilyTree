namespace FamilyTree_UI.Shared.Managers
{
    public class AuthUtilityEndPoint
    {
        public static string DropDownListFilter(string DropDownType, string? Filter1 = null, string? Filter2 = null)
        {
            return $"{DropDownList}?DropDownType={DropDownType}&Filter1={Filter1}&Filter2={Filter2}";
        }
        public static string DropDownList = "https://localhost:7140/api/Utility/GetDropDownList";
    }
}

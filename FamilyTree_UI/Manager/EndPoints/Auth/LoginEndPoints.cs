namespace FamilyTree_UI.Manager.EndPoints.Auth
{
    public static class LoginEndPoints
    {
        private static string _baseUrl;
        public static void Initialize(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        public static string GetLoginDetails => $"{_baseUrl}Login/GetLoginDetails";
    }
}

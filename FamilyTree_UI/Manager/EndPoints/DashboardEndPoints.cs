namespace FamilyTree_UI.Manager.EndPoints
{

    public static class DashboardEndPoints
    {
        private static string _baseUrl;
        public static void Initialize(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        public static string GetDashboardData => $"{_baseUrl}DashBoard/GetDashboardData";
        public static string GetUpcommingAnniversary => $"{_baseUrl}DashBoard/GetUpcommingAnniversary";
        public static string GetLatestBlogsPost => $"{_baseUrl}DashBoard/GetLatestBlogsPost";
        public static string GetHeadres => $"{_baseUrl}DashBoard/GetHeadres";
    }
}

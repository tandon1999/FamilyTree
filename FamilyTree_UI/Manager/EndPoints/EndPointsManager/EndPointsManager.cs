using FamilyTree_UI.Manager.EndPoints.Auth;
using FamilyTreeUI.Manager.EndPoints;

namespace FamilyTree_UI.Manager.EndPoints.EndPointsManager
{
    public static class EndPointsManager
    {
        public static void InitializeAllEndPoints(string baseUrl)
        {
            BlogsEndPoints.Initialize(baseUrl);
            DashboardEndPoints.Initialize(baseUrl);
            FamilyTreeMemberEndPoints.Initialize(baseUrl);
            SetupPagesEndPoints.Initialize(baseUrl);
            LoginEndPoints.Initialize(baseUrl);
        }
    }
}

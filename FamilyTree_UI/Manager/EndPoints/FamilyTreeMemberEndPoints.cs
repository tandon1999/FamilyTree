namespace FamilyTreeUI.Manager.EndPoints
{
    public static class FamilyTreeMemberEndPoints
    {
        private static string _baseUrl;
        public static void Initialize(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        public static string CreateFamilyTreeMember => $"{_baseUrl}FamilyMember/CreateFamilyMember";
        public static string GetFamilyTreeMembers => $"{_baseUrl}FamilyMember/GetAllFamilyMember";
        public static string GetFamilyMemberTimeLine => $"{_baseUrl}FamilyMember/GetFamilyMemberTimeLine";
        public static string DeleteFamilyTreeMember(int Id)
        {
            return $"{_baseUrl}FamilyMember/DeleteFamilyMember?Id={Id}";
        }

        public static string GetFamilyTreeMemberByid(int Id)
        {
            return $"{_baseUrl}FamilyMember/GetFamilyMemberById?Id={Id}";
        }

        public static string FamilyDetailsByParentId(int Id)
        {
            return $"{_baseUrl}FamilyMember/FamilyDetailsByParentId?Id={Id}";
        }
        public static string GetFamilyDetailsById(int Id)
        {
            return $"{_baseUrl}FamilyMember/GetFamilyDetailsById?Id={Id}";
        }
    }
}

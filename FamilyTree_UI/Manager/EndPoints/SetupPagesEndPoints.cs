namespace FamilyTree_UI.Manager.EndPoints
{
    public static class SetupPagesEndPoints
    {
        private static string _baseUrl;
        public static void Initialize(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        public static string CreateGallerySetup => $"{_baseUrl}SetupPages/CreateGallerySetup";
        public static string GetAllGalleries => $"{_baseUrl}SetupPages/GetAllGalleries";
        public static string GetHistoryDetails => $"{_baseUrl}SetupPages/GetHistoryDetails";
        public static string GetGalleryImageById(int Id)
        {
            return $"{_baseUrl}SetupPages/GetGalleryImageById?Id={Id}";
        }
        public static string DeleteGalleryImage(int Id)
        {
            return $"{_baseUrl}SetupPages/DeleteGalleyImage?Id={Id}";
        }

    }
}

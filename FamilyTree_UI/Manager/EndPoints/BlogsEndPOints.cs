namespace FamilyTree_UI.Manager.EndPoints
{
    public static class BlogsEndPoints
    {
        private static string _baseUrl;
        public static void Initialize(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        public static string CreateBlogsPost => $"{_baseUrl}Blogs/CreateBlogsPost";
        public static string GetAllBlogsPost => $"{_baseUrl}Blogs/GetAllBlogsPost";

        public static string GetBlogsPostById(int Id)
        {
            return $"{_baseUrl}Blogs/GetBlogsPostById?Id={Id}";
        }

        public static string DeleteBlogsPost(int Id)
        {
            return $"{_baseUrl}Blogs/DeleteBlogsPost?Id={Id}";
        }
    }
}

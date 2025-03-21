namespace FamilyTree_UI.ViewModels
{
    public class DashBoardViewModel
    {
       public string? DashBoardData { get; set; }
        public Dictionary<string, object>? Generations { get; set; }
    }
    public class UpcommingAnniVModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DeathDate { get; set; }
        public int DaysLeft { get; set; }
        public string? AnniversaryType { get; set; }
        public byte[]? ImageByte { get; set; }
        public string? ImageSrc { get; set; }
    }
    public class LastestBlogPostVModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? TimeAgo { get; set; }
        public byte[]? ImageByte { get; set; }
        public string? ImageSrc { get; set; }
    }

    public class HeadersVModel
    {

        public string? FamilyTree { get; set; }
        public string? FamilyDictionary { get; set; }
        public string? FamilyTimeLine { get; set; }
        public string? FamilyHistory { get; set; }
    }
}

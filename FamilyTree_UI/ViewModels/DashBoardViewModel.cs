namespace FamilyTree_UI.ViewModels
{
    public class DashBoardViewModel
    {
        public int TotalFamilyMember { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int Living { get; set; }
        public int Death { get; set; }
        public decimal AverageLifespan { get; set; }
        public string? LongestLivingIndividual { get; set; }
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

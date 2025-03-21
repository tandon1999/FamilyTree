namespace FamilyTreeApi.ResponseModel
{
    public class DashBoardResponseModel
    {
        public string? DashBoardData { get; set; }
    }

    public class UpcommingAnniResponseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DeathDate { get; set; }
        public int DaysLeft { get; set; }
        public string? AnniversaryType { get; set; }
        public byte[]? ImageByte { get; set; }
    }
    public class LastestBlogPostResponseModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? TimeAgo { get; set; }
        public byte[]? ImageByte { get; set; }
    }

    public class HeadersResponseModel
    {

        public string? FamilyTree { get; set; }
        public string? FamilyDictionary { get; set; }
        public string? FamilyTimeLine { get; set; }
        public string? FamilyHistory { get; set; }
    }

}

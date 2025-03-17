namespace FamilyTreeApi.Param
{
    public class BlogsParam
    {
        public char Flag { get; set; }
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ReadMoreLink { get; set; }
        public string? ImagePath { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

    }
}

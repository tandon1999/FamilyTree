namespace FamilyTree_UI.ViewModels
{
    public class BlogsPostVModel
    {
        public byte[]? ImageByte { get; set; }
        public string? ImageSrc { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Id { get; set; }
    }
}

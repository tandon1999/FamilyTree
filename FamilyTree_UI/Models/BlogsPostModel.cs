using FamilyTree_UI.Shared;

namespace FamilyTree_UI.Models
{
    public class BlogsPostModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ReadMoreLink { get; set; }
        public string? ImagePath { get; set; }
        public byte[]? ImageByte { get; set; }
        public ImageUpload? imageUpload { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
    }
}

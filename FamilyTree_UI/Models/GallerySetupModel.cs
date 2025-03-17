using FamilyTree_UI.Shared;

namespace FamilyTree_UI.Models
{
    public class GallerySetupModel
    {
        public int TempId { get; set; }
        public int Id { get; set; }
        public string? PhotoName { get; set; }
        public string? Description { get; set; }
        public int Category { get; set; }
        public DateTime DateofPhoto { get; set; }= DateTime.Now;
        public ImageUpload? imageUpload { get; set; }
        public byte[]? ImageByte { get; set; }
        public string? ImagePath { get; set; }
        public List<GallerySetupModel> gallerysetupList { get; set; } = new();
    }
}

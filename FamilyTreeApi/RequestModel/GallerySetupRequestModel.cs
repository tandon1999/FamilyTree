using FamilyTreeApi.Shared.Model;

namespace FamilyTreeApi.RequestModel
{
    public class GallerySetupRequestModel
    {
        public int Id { get; set; }
        public string? PhotoName { get; set; }
        public string? Description { get; set; }
        public int Category { get; set; }
        public DateTime DateofPhoto { get; set; }= DateTime.Today;
        public ImageUpload? imageUpload { get; set; }
        public string? ImagePath { get; set; }
        public byte[]? ImageByte { get; set; }
        public string? allimagedetails { get; set; }
        public List<GallerySetupRequestModel> gallerysetupList { get; set; } = new();

    }
}

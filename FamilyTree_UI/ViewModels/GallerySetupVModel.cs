namespace FamilyTree_UI.ViewModels
{
    public class GallerySetupVModel
    {
        public int Id { get; set; }
        public string? PhotoName { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public string? ImagePath { get; set; }
        public DateTime DateofPhoto { get; set; }
        public byte[]? ImageByte { get; set; }
        public string? ImageSrc { get; set; }
        public bool IsDetailsVisible { get; set; } = false;
    }
}

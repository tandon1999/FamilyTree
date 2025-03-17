namespace FamilyTreeApi.ResponseModel
{
    public class GallerySetupResponseModel
    {
        public int Id { get; set; }
        public string? PhotoName { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public string? ImagePath { get; set; }
        public DateTime DateofPhoto { get; set; }
        public byte[]? ImageByte { get; set; }
    }
}

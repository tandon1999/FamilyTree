namespace FamilyTreeApi.Shared.Model
{
    public class ImageUpload
    {
        public string? FileName { get; set; }
        public string? Extension { get; set; }
        public byte[]? FileByte { get; set; }
    }
}

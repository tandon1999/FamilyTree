namespace FamilyTreeApi.ResponseModel
{
    public class TimelineResponseModel
    {
        public string? FullName { get; set; }
        public DateTime? Date { get; set; }
        public string? ImagePath { get; set; }
        public byte[]? ImageByte { get; set; }
        public int Type { get; set; }
        public int Id { get; set; }

    }
}

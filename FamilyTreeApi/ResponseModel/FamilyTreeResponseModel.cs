namespace FamilyTreeApi.ResponseModel
{
    public class FamilyTreeResponseModel
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int FatherId { get; set; }
        public string? FatherName { get; set; }
        public int MotherId { get; set; }
        public string? MotherName { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DeathDate { get; set; }
        public string? ImagePath { get; set; }
        public byte[]? ImageByte { get; set; }
        public int WifeId { get; set; }
        public int HusbandId { get; set; }
        public int GenderId { get; set; }
        public string? SonIds { get; set; }
        public string? SonNames { get; set; }
        public string? DaughterIds { get; set; }
        public string? DaughterNames { get; set; }
        public int HasWife { get; set; }
        public int GenerationId { get; set; }
        public string? GenerationType { get; set; }
        public string? Identification { get; set; }
        public int IdentificationID { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

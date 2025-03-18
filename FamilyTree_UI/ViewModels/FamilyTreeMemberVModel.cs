namespace FamilyTreeUI.ViewModels
{
    public class FamilyTreeMemberVModel
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public string? GenderName { get; set; }
        public int GenderId { get; set; }
        public string? OccupationType { get; set; }
        public string? Description { get; set; }
        public DateTime DeathDate { get; set; }
        public string? MatrialStatus { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? Address { get; set; }
        public string? ImagePath { get; set; }
        public byte[]? ImageByte { get; set; }
        public string? ImageSrc { get; set; }

        public int FatherId { get; set; }
        public int MotherId { get; set; }
        public int NumberOfChildren { get; set; }
        public string? NepDOBYear { get; set; }
        public string? NepDeathYear { get; set; }
        public int HasValue { get; set; }
        public string? HusbandName { get; set; }
        public string? WifeName { get; set; }
    }
}

using FamilyTree_UI.Shared;

namespace FamilyTreeUI.Models
{
    public class FamilyMemberSetupModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; } = "टन्डन";
        public DateTime DOB { get; set; } = DateTime.Now;
        public int Age { get; set; }
        public int Gender { get; set; } 
        public int Occupation { get; set; }
        public int FatherName { get; set; } 
        public int MotherName { get; set; } 
        public int WIfeId { get; set; } 
        public int HusbandId { get; set; } 
        public string? Description { get; set; }
        public DateTime? DeathDate { get; set; }
        public int MatrialStatus { get; set; } 
        public int NumberOfChildren { get; set; }
        public int GenerationType { get; set; }
        public bool IsDeath { get; set; } = false;
        public string? Address { get; set; }
        public string? ImagePath { get; set; }
        public ImageUpload? imageUpload { get; set; }
        public byte[]? ImageByte { get; set; }
    }
}

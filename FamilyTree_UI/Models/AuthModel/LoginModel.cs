namespace FamilyTree_UI.Models.AuthModel
{
    public class LoginModel
    {
       // public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        //public string? Email { get; set; }
    }
    public class LoginTokenModel
    {
        public string UserName { get; set; } = string.Empty;
        public string JwtToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
        public bool IsMultiBranch { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public DateTime OperationDate { get; set; }
    }
}

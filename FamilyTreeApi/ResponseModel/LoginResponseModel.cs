namespace FamilyTreeApi.ResponseModel
{
    public class LoginResponseModel
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
    public class UserDataResponseModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Hash { get; set; } = string.Empty;
        public string? Salt { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
        public string? RefreshToken { get; set; } = string.Empty;
    }
}

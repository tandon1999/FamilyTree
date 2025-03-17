namespace FamilyTreeApi.Param.Auth
{
    public class LoginParam
    {
        public char Flag { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}

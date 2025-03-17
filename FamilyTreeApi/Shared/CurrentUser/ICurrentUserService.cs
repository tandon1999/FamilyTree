namespace FamilyTreeApi.Shared.CurrentUser
{
    public interface ICurrentUserService
    {
        string? Name { get; }
        string? UserName { get; }
        int? UserId { get; }
        string? Role { get; }
        int? BranchId { get; }
        string? BranchType { get; }
    }
}

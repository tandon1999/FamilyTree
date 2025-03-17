using System.Security.Claims;

namespace FamilyTreeApi.Shared.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            Name = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            UserId = string.IsNullOrEmpty(httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId"))
                               ? null
                               : Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId"));
            Role = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
            BranchType = httpContextAccessor.HttpContext?.User?.FindFirstValue("BranchType");
            BranchId = string.IsNullOrEmpty(httpContextAccessor.HttpContext?.User?.FindFirstValue("BranchID"))
                               ? null
                               : Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue("BranchId"));
        }
        public string? Name { get; }
        public string? UserName { get; }
        public int? UserId { get; }
        public string? Role { get; }
        public int? BranchId { get; }
        public string? BranchType { get; }
    }
}

using System.Security.Claims;

namespace FamilyTree_UI.Configuration.Extension
{
    internal static class ClaimsPrincipalExtensions
    {
        internal static string GetUserName(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue("UserName") ?? string.Empty;

        internal static int GetUserId(this ClaimsPrincipal claimsPrincipal)
            => int.TryParse(claimsPrincipal.FindFirstValue("UserId"), out var id) ? id : 0;

        internal static string GetFullName(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue("FullName") ?? string.Empty;

        internal static string GetEmail(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue("Email") ?? string.Empty;

        internal static string GetContactNumber(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue("ContactNumber") ?? string.Empty;

        internal static string GetCountry(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue("Country") ?? string.Empty;

        internal static string GetState(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue("State") ?? string.Empty;

        internal static string GetCity(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue("City") ?? string.Empty;

        internal static string GetAddress(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue("Address") ?? string.Empty;

        internal static int GetZipCode(this ClaimsPrincipal claimsPrincipal)
            => int.TryParse(claimsPrincipal.FindFirstValue("ZipCode"), out var zip) ? zip : 0;

        internal static string GetGenderType(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue("GenderType") ?? string.Empty;

        internal static string GetHash(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue("Hash") ?? string.Empty;

        internal static string GetSalt(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue("Salt") ?? string.Empty;

        internal static int GetRoleId(this ClaimsPrincipal claimsPrincipal)
            => int.TryParse(claimsPrincipal.FindFirstValue("RoleId"), out var roleId) ? roleId : 0;

        internal static string GetRoleName(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue("RoleName") ?? string.Empty;
    }


}

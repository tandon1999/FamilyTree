using FamilyTree_UI.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using System.Text.Json;

namespace FamilyTree_UI.Configuration.Extension
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _sessionStorage;
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
        public CustomAuthenticationStateProvider(ProtectedLocalStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var TokenStorageResult = await _sessionStorage.GetAsync<LoginViewModel>("Token");
                var userSession = TokenStorageResult.Success ? TokenStorageResult.Value : null;
                if (userSession == null)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }

                var claims = GetClaimsFromJwt(TokenStorageResult.Value!.Token);

                var ClaimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new(ClaimTypes.Email, "sumantandan1999@gmail.com"),
                }.Union(claims)
                , "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(ClaimPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }
        public async Task UpadteAuthenticationState(LoginViewModel tokenModel)
        {
            ClaimsPrincipal claimsPrincipal = new();
            if (tokenModel != null)
            {
                await _sessionStorage.SetAsync("Token", tokenModel);
                var claims = GetClaimsFromJwt(tokenModel.Token);

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new(ClaimTypes.Email, "sumantandan1999@gmail.com")
                }.Union(claims)
                ));
            }
            else
            {
                if ((await _sessionStorage.GetAsync<LoginViewModel>("Token")).Success)
                {
                    await _sessionStorage.DeleteAsync("Token");
                }
                claimsPrincipal = _anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        public async Task<ClaimsPrincipal> CurrentUser()
        {
            var state = await this.GetAuthenticationStateAsync();
            return state.User;

        }
        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));

            NotifyAuthenticationStateChanged(authState);
        }

        internal static IEnumerable<Claim> GetClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs != null)
            {
                keyValuePairs.TryGetValue(ClaimTypes.Role, out var roles);

                if (roles != null)
                {
                    if (roles.ToString()!.Trim().StartsWith("["))
                    {
                        var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString()!);

                        claims.AddRange(parsedRoles!.Select(role => new Claim(ClaimTypes.Role, role)));
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, roles.ToString()!));
                    }

                    keyValuePairs.Remove(ClaimTypes.Role);
                }

                keyValuePairs.TryGetValue("Permission", out var permissions);
                if (permissions != null)
                {
                    if (permissions.ToString()!.Trim().StartsWith("["))
                    {
                        var parsedPermissions = JsonSerializer.Deserialize<string[]>(permissions.ToString()!);
                        claims.AddRange(parsedPermissions!.Select(permission => new Claim("Permission", permission)));
                    }
                    else
                    {
                        claims.Add(new Claim("Permission", permissions.ToString()!));
                    }
                    keyValuePairs.Remove("Permission");
                }

                claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));
            }
            return claims;
        }
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            return Convert.FromBase64String(base64);
        }
    }

}

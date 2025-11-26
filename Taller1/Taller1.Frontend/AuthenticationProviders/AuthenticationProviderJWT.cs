using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Taller1.Frontend.Helpers;
using Taller1.Frontend.Services;

namespace Taller1.Frontend.AuthenticationProviders
{
    public class AuthenticationProviderJWT : AuthenticationStateProvider, ILoginService
    {
        private readonly IJSRuntime _jSRuntime;
        private readonly HttpClient _httpClient;
        private readonly string _tokenKey;
        private readonly AuthenticationState _anonymous;

        public AuthenticationProviderJWT(IJSRuntime jSRuntime, HttpClient httpClient)
        {
            _jSRuntime = jSRuntime;
            _httpClient = httpClient;
            _tokenKey = "TOKEN_KEY";
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var tokenObj = await _jSRuntime.GetLocalStorage(_tokenKey);

                if (tokenObj == null)
                {
                    return _anonymous;
                }

                var token = tokenObj.ToString();

                if (string.IsNullOrEmpty(token))
                {
                    return _anonymous;
                }

                return BuildAuthenticationState(token);
            }
            catch (Exception ex)
            {
                return _anonymous;
            }
        }

        public async Task LoginAsync(string token)
        {
            await _jSRuntime.SetLocalStorage(_tokenKey, token);

            var authState = BuildAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task LogoutAsync()
        {
            await _jSRuntime.RemoveLocalStorage(_tokenKey);
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
        }

        private AuthenticationState BuildAuthenticationState(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var claims = ParseClaimsFromJWT(token);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        }

        private IEnumerable<Claim> ParseClaimsFromJWT(string token)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var unserializedToken = jwtSecurityTokenHandler.ReadJwtToken(token);
            return unserializedToken.Claims;
        }
    }
}
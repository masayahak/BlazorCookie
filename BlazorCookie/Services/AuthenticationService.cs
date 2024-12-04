using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorCookie.Services
{
    public class AuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<bool> LoginAsync(LoginUser loginUser)
        {

            var claims = new List<Claim>();

            if (loginUser.UserName == "admin@test.com" && loginUser.Password == "test")
            {
                claims.Add(new Claim(ClaimTypes.Name, loginUser.UserName));
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }
            else if (loginUser.UserName == "user@test.com" && loginUser.Password == "test")
            {
                claims.Add(new Claim(ClaimTypes.Name, loginUser.UserName));
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }
            else
            {
                return false;
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                throw new InvalidOperationException("HttpContext is not available.");

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
            return true;

        }
        public async Task LogoutAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                throw new InvalidOperationException("HttpContext is not available.");

            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
    public class LoginUser
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
    }

}


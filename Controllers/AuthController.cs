using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class AuthController : Controller
{
    // GET: /Auth/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Auth/Login
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        // ===== MOCK API CALL =====
        var apiResult = MockLoginApi(username, password);

        if (!apiResult.Success)
        {
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        // ===== CREATE TOKEN (CLAIMS) =====
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, apiResult.Username),
            new Claim("AccessToken", apiResult.Token)
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal
        );

        return RedirectToAction("Home", "User");
    }

    // LOGOUT
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        return RedirectToAction("Login");
    }

    // GET: /Auth/Register
    public IActionResult Register()
    {
        return View();
    }

    // ============================
    // MOCK API (TEMP)
    // ============================
    private (bool Success, string Username, string Token) MockLoginApi(string username, string password)
    {
        if (username == "user" && password == "1234")
        {
            return (true, "user", "mock-token-user-123");
        }

        if (username == "admin" && password == "admin")
        {
            return (true, "admin", "mock-token-admin-999");
        }

        return (false, null, null);
    }
}

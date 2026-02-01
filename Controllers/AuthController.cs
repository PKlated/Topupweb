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
        // ===== MOCK LOGIN =====
        var apiResult = MockLogin(username, password);

        if (!apiResult.Success)
        {
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        if (apiResult.AccountStatus == "Banned")
        {
            ViewBag.Error = "This account has been banned.";
            return View();
        }

        // ===== CLAIMS =====
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, apiResult.UserId.ToString()),
            new Claim(ClaimTypes.Name, apiResult.Username),
            new Claim(ClaimTypes.Role, apiResult.Role),
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity)
        );

        // ===== REDIRECT =====
        return apiResult.Role switch
        {
            "SuperAdmin" => RedirectToAction("AccountManagement", "SuperAdmin"),
            "GameAdmin"  => RedirectToAction("Profit", "SuperAdmin"),
            "GameMaster" => RedirectToAction("RequestReport", "SuperAdmin"),
            _            => RedirectToAction("Home", "User")
        };
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        return RedirectToAction("Login");
    }

    // ============================
    // MOCK DATA
    // ============================
    private (bool Success, int UserId, string Username, string Role, string AccountStatus)
        MockLogin(string username, string password)
    {
        if (username == "user" && password == "1234")
            return (true, 1, "user", "User", "Active");

        if (username == "gm" && password == "gm")
            return (true, 2, "gm", "GameMaster", "Active");

        if (username == "admin" && password == "admin")
            return (true, 3, "admin", "GameAdmin", "Active");

        if (username == "super" && password == "super")
            return (true, 4, "super", "SuperAdmin", "Active");

        if (username == "banned" && password == "banned")
            return (true, 5, "banned", "User", "Banned");

        return (false, 0, null, null, null);
    }
}

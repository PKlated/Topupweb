using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthApiController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(string username, string password)
    {
        var result = MockLogin(username, password);

        if (!result.Success)
            return Unauthorized(new { message = "Invalid username or password" });

        return Ok(new
        {
            result.UserId,
            result.Username,
            result.Role,
            result.AccountStatus,
            result.Token
        });
    }

    // =========================
    // MOCK DATA (TEMP ONLY)
    // =========================
    private (bool Success,
             int UserId,
             string Username,
             string Role,
             string AccountStatus,
             string Token)
        MockLogin(string username, string password)
    {
        // NORMAL USER
        if (username == "user" && password == "1234")
        {
            return (true, 1, "user", "User", "Active", "token-user-111");
        }

        // GAME MASTER
        if (username == "gm" && password == "gm")
        {
            return (true, 2, "gm", "GameMaster", "Active", "token-gm-222");
        }

        // GAME ADMIN
        if (username == "admin" && password == "admin")
        {
            return (true, 3, "admin", "GameAdmin", "Active", "token-admin-333");
        }

        // SUPER ADMIN
        if (username == "super" && password == "super")
        {
            return (true, 4, "super", "SuperAdmin", "Active", "token-super-444");
        }

        // BANNED USER EXAMPLE
        if (username == "banned" && password == "1234")
        {
            return (true, 5, "banned", "User", "Banned", "token-banned-555");
        }

        return (false, 0, null, null, null, null);
    }
}

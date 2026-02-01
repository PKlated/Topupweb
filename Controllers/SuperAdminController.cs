using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize] // must be logged in
public class SuperAdminController : Controller
{
    // =====================
    // COMMON (ALL ADMINS)
    // =====================

    [Authorize(Roles = "SuperAdmin,GameAdmin")]
    public IActionResult Profit()
    {
        return View();
    }

    [Authorize(Roles = "SuperAdmin,GameAdmin")]
    public IActionResult UserPaymentLog()
    {
        return View();
    }

    [Authorize(Roles = "SuperAdmin,GameAdmin")]
    public IActionResult TopupPriceEdit()
    {
        return View();
    }

    [Authorize(Roles = "SuperAdmin,GameAdmin")]
    public IActionResult UserList()
    {
        return View();
    }

    // =====================
    // GAME MASTER
    // =====================

    [Authorize(Roles = "SuperAdmin,GameMaster")]
    public IActionResult RequestReport()
    {
        return View();
    }

    [Authorize(Roles = "SuperAdmin,GameMaster")]
    public IActionResult PlayerList()
    {
        return View();
    }

    [Authorize(Roles = "SuperAdmin,GameMaster")]
    public IActionResult GameServerStatus()
    {
        return View();
    }

    // =====================
    // SUPER ADMIN ONLY
    // =====================

    [Authorize(Roles = "SuperAdmin")]
    public IActionResult AccountManagement()
    {
        return View();
    }
}

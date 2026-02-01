using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "GameMaster,SuperAdmin")]
public class GameMasterController : Controller
{
    // 🔹 Request report page
    public IActionResult RequestReport()
    {
        return View("~/Views/SuperAdmin/RequestReport.cshtml");
    }

    // 🔹 Player list page
    public IActionResult PlayerList()
    {
        return View("~/Views/SuperAdmin/PlayerList.cshtml");
    }

    // 🔹 Inspect player inventory
    public IActionResult PlayerInventory(int id)
    {
        return View("~/Views/SuperAdmin/PlayerInventory.cshtml");
    }

    // 🔹 Game server status + global announcement
    public IActionResult ServerStatus()
    {
        return View("~/Views/SuperAdmin/ServerStatus.cshtml");
    }

    // 🔹 Global announcement action
    [HttpPost]
    public IActionResult GlobalAnnouncement(string message)
    {
        // later: send to game server
        TempData["Success"] = "Global announcement sent.";
        return RedirectToAction("ServerStatus");
    }
}

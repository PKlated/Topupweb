using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class UserController : Controller
{
    // =====================
    // PUBLIC (Guest + User)
    // =====================

    public IActionResult Home()
    {
        return View();
    }

    public IActionResult Event()
    {
        return View();
    }

    public IActionResult Download()
    {
        return View();
    }

    // =====================
    // AUTH REQUIRED
    // =====================

    [Authorize]
    public IActionResult TopUp()
    {
        return View();
    }

    [Authorize]
    public IActionResult PaymentHistory()
    {
        return View();
    }

    [Authorize]
    [Authorize]
public IActionResult PaymentRemaining(int id)
{
    // TEMP MOCK DATA (replace with DB later)
    var payments = new[]
    {
        new { Id = 1, Package = "100 Gems", Amount = 49, Status = "Success" },
        new { Id = 2, Package = "500 Gems", Amount = 199, Status = "Success" },
        new { Id = 3, Package = "1000 Gems", Amount = 399, Status = "Pending" }
    };

    var payment = payments.FirstOrDefault(p => p.Id == id);

    if (payment == null)
        return RedirectToAction("PaymentHistory");

    ViewBag.Package = payment.Package;
    ViewBag.Amount = payment.Amount;
    ViewBag.Status = payment.Status;

    return View();
}


    [Authorize]
    public IActionResult Inventory()
    {
        return View();
    }

    [Authorize]
    public IActionResult Setting()
    {
        return View();
    }
}

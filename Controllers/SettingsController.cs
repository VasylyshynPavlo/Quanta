using Microsoft.AspNetCore.Mvc;

namespace Quanta.Controllers;

public class SettingsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
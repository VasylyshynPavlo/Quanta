using Microsoft.AspNetCore.Mvc;

namespace Quanta.Controllers;

public class ChatController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
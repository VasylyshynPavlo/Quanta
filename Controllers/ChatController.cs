using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Quanta.Controllers;

[Authorize]
public class ChatController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
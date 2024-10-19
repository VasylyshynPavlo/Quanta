using System.Diagnostics;
using Data.Data;
using Microsoft.AspNetCore.Mvc;
using Quanta.Models;

namespace Quanta.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private QuantaDbContext context = new();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var posts = context.Posts.ToList();
        return View(posts);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult DeletePostFromDb(int id)
    {
        var post = context.Posts.Find(id);
        if (post == null) return NotFound();
        context.Posts.Remove(post);
        context.SaveChanges();
        return RedirectToAction("Index");
    }
    
    public static string GetTimeAgo(DateTime dateTime)
    {
        var timeSpan = DateTime.Now - dateTime;

        if (timeSpan.TotalSeconds < 60)
        {
            return $"{(int)timeSpan.TotalSeconds} seconds ago";
        }
        else if (timeSpan.TotalMinutes < 60)
        {
            return $"{(int)timeSpan.TotalMinutes} minutes ago";
        }
        else if (timeSpan.TotalHours < 12)
        {
            return $"{(int)timeSpan.TotalHours} hours ago";
        }
        else
        {
            return dateTime.ToString("yyyy.MM.dd | HH:mm"); // Повертаємо дату, якщо більше 12 годин
        }
    }
}
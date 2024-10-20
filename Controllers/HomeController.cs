using System.Diagnostics;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Quanta.Models;

namespace Quanta.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private QuantaDbContext context = new();
    private readonly UserManager<User> _userManager;
    public HomeController(ILogger<HomeController> logger, UserManager<User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var posts = context.Posts.ToList();
        
        var postWithUsernames = posts.Select(post => new PostWithUsernameViewModel
        {
            Post = post,
            Username = _userManager.Users.FirstOrDefault(u => u.Id == post.UserId)?.UserName ?? "Deleted"
        }).ToList();

        return View(postWithUsernames);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
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

    [HttpPost]
    public IActionResult Create(Post model)
    {
        model.UserId = _userManager.GetUserId(User);
        model.Created = DateTime.Now;
        context.Posts.Add(model);
        context.SaveChanges();
        return RedirectToAction("Index");
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
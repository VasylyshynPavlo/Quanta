using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Quanta.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly QuantaDbContext _context;

        public ChatController(QuantaDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetChatHistory(string userId)
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            
            var messages = await _context.ChatMessages
                .Where(m => (m.SenderId == currentUserId && m.ReceiverId == userId) ||
                            (m.SenderId == userId && m.ReceiverId == currentUserId))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            return Json(new { messages });
        }

        public async Task<IActionResult> GetUsersInvolved()
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            
            var messages = await _context.ChatMessages.ToListAsync();
            var involvedUserIds = new HashSet<string>();

            foreach (var message in messages)
            {
                if (message.SenderId == currentUserId)
                {
                    involvedUserIds.Add(message.ReceiverId);
                }
                else if (message.ReceiverId == currentUserId)
                {
                    involvedUserIds.Add(message.SenderId); 
                }
            }
            
            var users = await _context.Users
                .Where(u => involvedUserIds.Contains(u.Id))
                .Select(u => new 
                {
                    u.Id,
                    u.UserName,
                    u.Avatar
                })
                .ToListAsync();

                Console.WriteLine($"Involved User IDs: {string.Join(", ", users)}");
                return Json(users);
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}

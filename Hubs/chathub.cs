using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.SignalR;


namespace Quanta.Hubs
{

    public class ChatHub : Hub
    {
        private readonly QuantaDbContext _context;

        public ChatHub(QuantaDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string userId, string message)
        {
            var chatMessage = new ChatMessage
            {
                SenderId = Context.UserIdentifier,
                ReceiverId = userId,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            // Надсилаємо повідомлення іншому користувачу
            await Clients.User(userId).SendAsync("ReceiveMessage", Context.UserIdentifier, message);
            // Надсилаємо повідомлення собі
            await Clients.Caller.SendAsync("ReceiveMessage", "You", message);
        }
    }
}
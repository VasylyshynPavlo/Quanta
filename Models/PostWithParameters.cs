using Data.Entities;

namespace Quanta.Models;

public class PostWithParameters
{
    public Post Post { get; set; }
    public string Username { get; set; }
    public string Avatar { get; set; }
}
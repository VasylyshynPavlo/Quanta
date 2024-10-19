using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class User : IdentityUser
{
    public string? Avatar { get; set; }
    public DateOnly? Birthday { get; set; }
    public string? FirstName { get; set; }
}
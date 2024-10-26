using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class User : IdentityUser
{
    [PersonalData]
    public string? Avatar { get; set; }
    [PersonalData]
    public DateOnly? Birthday { get; set; }
}
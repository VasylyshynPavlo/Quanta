using System.ComponentModel.DataAnnotations;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Quanta.Areas.Identity.Pages.Account.Manage;

public class AvatarModel : PageModel
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AvatarModel(
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public string Avatar { get; set; }
    
    [TempData]
    public string StatusMessage { get; set; }
    
    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        [DataType(DataType.ImageUrl)]
        public string Avatar { get; set; }
    }

    private async Task LoadAsync(User user)
    {
        Avatar = user.Avatar;
        Input = new InputModel
        {
            Avatar = Avatar
        };
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }
        
        await LoadAsync(user);
        return Page();
    }
    
    public async Task<IActionResult> OnPostChangeAvatarAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        if (!ModelState.IsValid)
        {
            await LoadAsync(user);
            return Page();
        }
        
        user.Avatar = Input.Avatar;
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            StatusMessage = "Your avatar has been updated.";
            return RedirectToPage();
        }
        
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        await LoadAsync(user);
        return Page();
    }
}
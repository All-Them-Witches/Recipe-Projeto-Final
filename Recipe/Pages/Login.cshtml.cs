using Application.Contracts.UserContracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Recipe.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserApplication _userApplication;

        public LoginModel(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [BindProperty]
        public LoginUserCommand user { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return Page();

            return RedirectToPage("/Info");
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = _userApplication.FindUser(u => u.Username == this.user.Username && u.Password == this.user.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Email,user.Email),
                    };
                    if (user.Roles != null)
                    {
                        foreach (var role in user.Roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.Role));
                        }
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, SampleRoles.user));
                    }
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    AuthenticationProperties authenticationProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Adjust the expiration time as needed
                    }; ;
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                    return RedirectToPage("Index");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return Page();
        }
    }
}

using Application.Contracts.UserContracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Recipe.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly IUserApplication _userApplication;

        public SignUpModel(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        public CreateUserCommand user { get; set; }
        public async Task<IActionResult> OnGet()
        {
            //This is for testing , at the last phase of the application it would be removed
            if (!User.Identity.IsAuthenticated)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/Info");
            }
        }
        public async Task<IActionResult> OnPostAsync(CreateUserCommand user)
        {
            if (ModelState.IsValid)
            {
                int? id;
                var registrationSuccessful = _userApplication.AddUser(user,out id);

                if (registrationSuccessful)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, SampleRoles.user),
                        new Claim(ClaimTypes.Email,user.Email)
                    };
                    if (id != null)
                        claims.Add(new Claim(ClaimTypes.NameIdentifier,id.Value.ToString()));

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddDays(30)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                    return RedirectToPage("Index");
                }
                ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
            }
            return Page();
        }
    }
}
